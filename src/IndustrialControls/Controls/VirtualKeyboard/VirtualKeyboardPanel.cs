using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IndustrialControls.Controls.VirtualKeyboard
{
    /// <summary>
    /// 虚拟键盘面板 - 使用绝对定位
    /// </summary>
    public class VirtualKeyboardPanel : UserControl
    {
        private KeyboardLayoutMode _layoutMode = KeyboardLayoutMode.QWERTY;
        private bool _isShift = false;
        private bool _isCaps = false;
        private KeyboardConfig _config = new KeyboardConfig();
        private Panel _buttonsContainer;
        private List<KeyButton> _allButtons = new List<KeyButton>();

        /// <summary>
        /// 按键输入事件
        /// </summary>
        public event EventHandler<string> KeyInput;

        /// <summary>
        /// 布局模式
        /// </summary>
        [Browsable(true)]
        [Category("行为")]
        [Description("获取或设置当前键盘布局模式")]
        [DefaultValue(KeyboardLayoutMode.QWERTY)]
        public KeyboardLayoutMode LayoutMode
        {
            get => _layoutMode;
            set
            {
                if (_layoutMode != value)
                {
                    _layoutMode = value;
                    RebuildLayout();
                }
            }
        }

        /// <summary>
        /// 键盘配置
        /// </summary>
        [Browsable(true)]
        [Category("配置")]
        [Description("获取或设置键盘配置")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KeyboardConfig Config
        {
            get => _config;
            set
            {
                _config = value ?? new KeyboardConfig();
                RebuildLayout();
            }
        }

        public VirtualKeyboardPanel()
        {
            InitializePanel();
            // 首次加载时必须构建布局
            RebuildLayout();
        }

        private void InitializePanel()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(248, 248, 248);
            // 移除固定的 MinimumSize，让布局计算决定尺寸
            
            // 创建按钮容器
            _buttonsContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoSize = false
            };
            Controls.Add(_buttonsContainer);
        }

        /// <summary>
        /// 重新构建布局
        /// </summary>
        public void RebuildLayout()
        {
            _buttonsContainer.Controls.Clear();
            _allButtons.Clear();

            var rows = _layoutMode == KeyboardLayoutMode.QWERTY 
                ? GetQwertyLayout() 
                : GetNumberPadLayout();

            LayoutButtons(rows);
        }

        /// <summary>
        /// 获取QWERTY布局定义
        /// </summary>
        private List<List<KeyDefinition>> GetQwertyLayout()
        {
            return new List<List<KeyDefinition>>
            {
                // 第1行：数字
                new List<KeyDefinition>
                {
                    new KeyDefinition("1", "1"),
                    new KeyDefinition("2", "2"),
                    new KeyDefinition("3", "3"),
                    new KeyDefinition("4", "4"),
                    new KeyDefinition("5", "5"),
                    new KeyDefinition("6", "6"),
                    new KeyDefinition("7", "7"),
                    new KeyDefinition("8", "8"),
                    new KeyDefinition("9", "9"),
                    new KeyDefinition("0", "0"),
                    new KeyDefinition("-", "-", true),
                    new KeyDefinition("=", "=", true),
                    new KeyDefinition("⌫", "Backspace", true, true)
                },
                // 第2行：QWERTY
                new List<KeyDefinition>
                {
                    new KeyDefinition("Tab", "Tab", true),
                    new KeyDefinition("Q", "q"),
                    new KeyDefinition("W", "w"),
                    new KeyDefinition("E", "e"),
                    new KeyDefinition("R", "r"),
                    new KeyDefinition("T", "t"),
                    new KeyDefinition("Y", "y"),
                    new KeyDefinition("U", "u"),
                    new KeyDefinition("I", "i"),
                    new KeyDefinition("O", "o"),
                    new KeyDefinition("P", "p"),
                    new KeyDefinition("[", "[", true),
                    new KeyDefinition("]", "]", true)
                },
                // 第3行：ASDF
                new List<KeyDefinition>
                {
                    new KeyDefinition("Caps", "Caps", true),
                    new KeyDefinition("A", "a"),
                    new KeyDefinition("S", "s"),
                    new KeyDefinition("D", "d"),
                    new KeyDefinition("F", "f"),
                    new KeyDefinition("G", "g"),
                    new KeyDefinition("H", "h"),
                    new KeyDefinition("J", "j"),
                    new KeyDefinition("K", "k"),
                    new KeyDefinition("L", "l"),
                    new KeyDefinition(";", ";", true),
                    new KeyDefinition("'", "'", true),
                    new KeyDefinition("Enter", "Enter", true)
                },
                // 第4行：ZXCV
                new List<KeyDefinition>
                {
                    new KeyDefinition("Shift", "Shift", true),
                    new KeyDefinition("Z", "z"),
                    new KeyDefinition("X", "x"),
                    new KeyDefinition("C", "c"),
                    new KeyDefinition("V", "v"),
                    new KeyDefinition("B", "b"),
                    new KeyDefinition("N", "n"),
                    new KeyDefinition("M", "m"),
                    new KeyDefinition(",", ",", true),
                    new KeyDefinition(".", ".", true),
                    new KeyDefinition("/", "/", true),
                    new KeyDefinition("Shift", "Shift", true)
                },
                // 第5行：功能键
                new List<KeyDefinition>
                {
                    new KeyDefinition("123", "123", true),
                    new KeyDefinition("Space", "Space", true),
                    new KeyDefinition("@", "@", true),
                    new KeyDefinition(".", ".", true),
                    new KeyDefinition("(", "(", true),
                    new KeyDefinition(")", ")", true)
                }
            };
        }

        /// <summary>
        /// 获取NumberPad布局定义
        /// </summary>
        private List<List<KeyDefinition>> GetNumberPadLayout()
        {
            return new List<List<KeyDefinition>>
            {
                new List<KeyDefinition>
                {
                    new KeyDefinition("ABC", "ABC", true),
                    new KeyDefinition("7", "7"),
                    new KeyDefinition("8", "8"),
                    new KeyDefinition("9", "9"),
                    new KeyDefinition("⌫", "Backspace", true, true)
                },
                new List<KeyDefinition>
                {
                    new KeyDefinition("+/-", "+/-", true),
                    new KeyDefinition("4", "4"),
                    new KeyDefinition("5", "5"),
                    new KeyDefinition("6", "6"),
                    new KeyDefinition("Enter", "Enter", true)
                },
                new List<KeyDefinition>
                {
                    new KeyDefinition("1", "1"),
                    new KeyDefinition("2", "2"),
                    new KeyDefinition("3", "3"),
                    new KeyDefinition(".", ".")
                },
                new List<KeyDefinition>
                {
                    //new KeyDefinition("0", "0"),
                    new KeyDefinition("0", "0"),
                    new KeyDefinition("-", "-"),
                    new KeyDefinition("+", "+")
                }
            };
        }

        /// <summary>
        /// 使用绝对定位布局按钮
        /// </summary>
        private void LayoutButtons(List<List<KeyDefinition>> rows)
        {
            int spacing = _config.ButtonSpacing;
            int padding = _config.PanelPadding;
            int buttonWidth = _config.ButtonWidth;
            int buttonHeight = _config.ButtonHeight;

            int currentY = padding;
            int maxWidth = 0;

            foreach (var row in rows)
            {
                int currentX = padding;

                foreach (var keyDef in row)
                {
                    var button = CreateButton(keyDef);
                    button.Location = new Point(currentX, currentY);
                    button.Size = new Size(buttonWidth, buttonHeight);
                    
                    _buttonsContainer.Controls.Add(button);
                    _allButtons.Add(button);

                    currentX += buttonWidth + spacing;
                }

                int rowWidth = padding * 2 + row.Count * (buttonWidth + spacing) - spacing;
                if (rowWidth > maxWidth) maxWidth = rowWidth;
                currentY += buttonHeight + spacing;
            }

            // 设置容器大小
            int totalWidth = maxWidth;
            int totalHeight = padding * 2 + rows.Count * (buttonHeight + spacing) - spacing;
            
            _buttonsContainer.MinimumSize = new Size(totalWidth, totalHeight);
            Size = new Size(totalWidth, totalHeight);
        }

        /// <summary>
        /// 创建按钮
        /// </summary>
        private KeyButton CreateButton(KeyDefinition keyDef)
        {
            var button = new KeyButton
            {
                Text = keyDef.DisplayText,
                KeyText = keyDef.KeyValue,
                IsSpecialKey = keyDef.IsSpecial,
                IsRepeatable = keyDef.IsRepeatable,
                Font = keyDef.IsSpecial ? _config.SpecialFont : _config.NormalFont
            };

            button.KeyPressed += OnKeyPressed;
            return button;
        }

        /// <summary>
        /// 按键处理
        /// </summary>
        private void OnKeyPressed(object sender, KeyButton button)
        {
            string key = button.KeyText;

            switch (key)
            {
                case "Backspace":
                case "Enter":
                case "Space":
                case "Tab":
                    KeyInput?.Invoke(this, key);
                    break;

                case "Shift":
                    _isShift = !_isShift;
                    UpdateKeyLabels();
                    break;

                case "Caps":
                    _isCaps = !_isCaps;
                    UpdateKeyLabels();
                    break;

                case "123":
                    LayoutMode = KeyboardLayoutMode.NumberPad;
                    break;

                case "ABC":
                    LayoutMode = KeyboardLayoutMode.QWERTY;
                    break;

                case "+/-":
                    KeyInput?.Invoke(this, "+/-");
                    break;

                default:
                    if (_isCaps || _isShift)
                    {
                        KeyInput?.Invoke(this, key.ToUpper());
                        if (_isShift) _isShift = false;
                    }
                    else
                    {
                        KeyInput?.Invoke(this, key.ToLower());
                    }
                    break;
            }
        }

        /// <summary>
        /// 更新按键标签
        /// </summary>
        private void UpdateKeyLabels()
        {
            foreach (var btn in _allButtons)
            {
                if (btn.Text.Length == 1 && char.IsLetter(btn.Text[0]))
                {
                    btn.Text = _isCaps || _isShift ? btn.Text.ToUpper() : btn.Text.ToLower();
                }
            }
        }
    }

    /// <summary>
    /// 按键定义
    /// </summary>
    public class KeyDefinition
    {
        public string DisplayText { get; set; }
        public string KeyValue { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsRepeatable { get; set; }

        public KeyDefinition(string displayText, string keyValue, bool isSpecial = false, bool isRepeatable = false)
        {
            DisplayText = displayText;
            KeyValue = keyValue;
            IsSpecial = isSpecial;
            IsRepeatable = isRepeatable;
        }
    }

}
