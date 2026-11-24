using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Core.Helpers;

/// <summary>
/// Dịch text tĩnh của control theo ngôn ngữ đang chọn (vi/en/ja).
/// Giữ nguyên bản gốc để khôi phục khi quay lại tiếng Việt.
/// </summary>
public static class UiTranslator
{
    private sealed class Slot
    {
        public string Original = string.Empty;
        public string Applied = string.Empty;
    }

    private static readonly ConditionalWeakTable<Control, Slot> ControlSlots = new();
    private static readonly ConditionalWeakTable<ToolStripItem, Slot> ItemSlots = new();
    private static readonly ConditionalWeakTable<DataGridViewColumn, Slot> ColumnSlots = new();
    private static readonly ConditionalWeakTable<Control, List<Slot>> ListSlots = new();
    private static readonly Dictionary<string, Dictionary<string, string>?> Dictionaries = new();
    private static readonly TimeSpan SweepInterval = TimeSpan.FromMilliseconds(300);

    private static string _language = "vi";
    private static DateTime _lastSweep = DateTime.MinValue;
    private static bool _started;

    public static void Initialize()
    {
        if (_started) return;
        _started = true;
        _language = NormalizeCulture(CultureHelper.CurrentCulture.Name);
        CultureHelper.CultureChanged += OnCultureChanged;
        Application.Idle += OnIdle;
    }

    /// <summary>Dịch toàn bộ cây control (dùng cho form/màn hình mới mở).</summary>
    public static void Apply(Control root) => ApplyTree(root);

    /// <summary>Dịch một chuỗi đơn lẻ; trả về nguyên văn nếu không có bản dịch.</summary>
    public static string Translate(string text)
    {
        if (string.IsNullOrEmpty(text) || _language == "vi") return text;
        return TryTranslate(text, out string translated) ? translated : text;
    }

    private static void OnCultureChanged()
    {
        // Không dịch ngay trong sự kiện: các form còn cập nhật text bằng resource riêng.
        // Để lần Application.Idle kế tiếp quét sau khi mọi handler đã chạy xong.
        _language = NormalizeCulture(CultureHelper.CurrentCulture.Name);
        _lastSweep = DateTime.MinValue;
    }

    private static void OnIdle(object? sender, EventArgs e)
    {
        try
        {
            DateTime now = DateTime.UtcNow;
            if (now - _lastSweep < SweepInterval) return;
            _lastSweep = now;

            // Quét lại định kỳ để dịch cả text được sinh ra lúc chạy (trạng thái, header...)
            foreach (Form form in Application.OpenForms)
            {
                ApplyTree(form);
            }
        }
        catch
        {
            // Không để lỗi dịch text làm sập giao diện
        }
    }

    private static string NormalizeCulture(string name)
    {
        if (name.StartsWith("en", StringComparison.OrdinalIgnoreCase)) return "en";
        if (name.StartsWith("ja", StringComparison.OrdinalIgnoreCase)) return "ja";
        return "vi";
    }

    private static void ApplyTree(Control root)
    {
        ApplyOne(root);
        foreach (Control child in root.Controls)
        {
            ApplyTree(child);
        }
    }

    private static void ApplyOne(Control control)
    {
        switch (control)
        {
            case Form form:
                TranslateControl(form, () => form.Text, value => form.Text = value);
                break;
            case Label label:
                TranslateControl(label, () => label.Text, value => label.Text = value);
                break;
            case ButtonBase button:
                TranslateControl(button, () => button.Text, value => button.Text = value);
                break;
            case GroupBox group:
                TranslateControl(group, () => group.Text, value => group.Text = value);
                break;
            case TabPage page:
                TranslateControl(page, () => page.Text, value => page.Text = value);
                break;
            case ToolStrip strip:
                ApplyToolStripItems(strip.Items);
                break;
            case DataGridView grid:
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    TranslateColumn(column);
                }
                break;
            case ListBox listBox when listBox.DataSource == null:
                ApplyStringItems(listBox, listBox.Items);
                break;
            case CheckedListBox checkedListBox when checkedListBox.DataSource == null:
                ApplyStringItems(checkedListBox, checkedListBox.Items);
                break;
            case ComboBox combo when combo.DataSource == null:
                ApplyStringItems(combo, combo.Items);
                break;
        }
    }

    private static void ApplyToolStripItems(ToolStripItemCollection items)
    {
        foreach (ToolStripItem item in items)
        {
            if (item is ToolStripComboBox) continue;

            TranslateItem(item);
            if (item is ToolStripMenuItem menuItem)
            {
                ApplyToolStripItems(menuItem.DropDownItems);
            }
        }
    }

    private static void TranslateControl(Control control, Func<string> get, Action<string> set)
    {
        var slot = ControlSlots.GetValue(control, _ => new Slot());
        ApplySlot(slot, get(), set);
    }

    private static void TranslateItem(ToolStripItem item)
    {
        var slot = ItemSlots.GetValue(item, _ => new Slot());
        ApplySlot(slot, item.Text, value => item.Text = value);
    }

    private static void TranslateColumn(DataGridViewColumn column)
    {
        var slot = ColumnSlots.GetValue(column, _ => new Slot());
        ApplySlot(slot, column.HeaderText, value => column.HeaderText = value);
    }

    private static void ApplySlot(Slot slot, string current, Action<string> set)
    {
        if (string.IsNullOrEmpty(current)) return;

        string baseText = slot.Applied.Length > 0 && current == slot.Applied
            ? slot.Original
            : current;

        if (_language == "vi")
        {
            if (baseText != current) set(baseText);
            slot.Original = baseText;
            slot.Applied = baseText;
            return;
        }

        if (TryTranslate(baseText, out string translated))
        {
            slot.Original = baseText;
            slot.Applied = translated;
            if (translated != current) set(translated);
            return;
        }

        // Đang hiển thị bản dịch cũ nhưng ngôn ngữ mới không có bản dịch cho text gốc:
        // thử dịch lại từ bản gốc, nếu vẫn không được thì trả về tiếng Việt.
        if (slot.Applied.Length > 0 && current == slot.Applied && slot.Original.Length > 0)
        {
            if (TryTranslate(slot.Original, out string retranslated))
            {
                slot.Applied = retranslated;
                set(retranslated);
            }
            else
            {
                set(slot.Original);
                slot.Applied = slot.Original;
            }
        }
    }

    private static void ApplyStringItems(Control owner, System.Collections.IList items)
    {
        var slots = ListSlots.GetValue(owner, _ => new List<Slot>());
        while (slots.Count < items.Count) slots.Add(new Slot());

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] is not string current || string.IsNullOrEmpty(current)) continue;

            var slot = slots[i];
            string baseText = slot.Applied.Length > 0 && current == slot.Applied
                ? slot.Original
                : current;

            if (_language == "vi")
            {
                if (baseText != current) items[i] = baseText;
                slot.Original = baseText;
                slot.Applied = baseText;
                continue;
            }

            if (TryTranslate(baseText, out string translated))
            {
                slot.Original = baseText;
                slot.Applied = translated;
                if (translated != current) items[i] = translated;
            }
        }
    }

    private static bool TryTranslate(string text, out string translated)
    {
        translated = text;
        if (_language == "vi") return false;

        var dictionary = GetDictionary();
        if (dictionary == null) return false;

        if (dictionary.TryGetValue(text, out string? value) && !string.IsNullOrEmpty(value) && value != text)
        {
            translated = value;
            return true;
        }
        return false;
    }

    private static Dictionary<string, string>? GetDictionary()
    {
        if (Dictionaries.TryGetValue(_language, out var cached)) return cached;

        Dictionary<string, string>? loaded = null;
        try
        {
            string name = $"Core.Resources.UiText.{_language}.json";
            using Stream? stream = typeof(UiTranslator).Assembly.GetManifestResourceStream(name);
            if (stream != null)
            {
                loaded = JsonSerializer.Deserialize<Dictionary<string, string>>(stream);
            }
        }
        catch
        {
            loaded = null;
        }

        Dictionaries[_language] = loaded;
        return loaded;
    }
}
