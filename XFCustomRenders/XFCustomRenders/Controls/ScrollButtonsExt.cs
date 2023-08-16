using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

using TouchSam;

using Xamarin.Forms;

namespace XFCustomRenders.Controls
{
    [ContentProperty(nameof(ItemTemplate))]
    public class ScrollButtonsExt : Layout<View>
    {
        private readonly ICommand commandTap;
        private ColumnDefinitionCollection widthRule;
        private View lastTapItem;

        public ScrollButtonsExt()
        {
            commandTap = new Command(ActionTap);
        }

        #region bindable props
        // ItemsSource
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(ScrollButtonsExt),
                null,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ScrollButtonsExt;

                    if (o is INotifyCollectionChanged obsOld)
                        obsOld.CollectionChanged -= self.OnSourceChanged;

                    if (n is INotifyCollectionChanged obsNew)
                        obsNew.CollectionChanged += self.OnSourceChanged;

                    self.InitItems();
                }
            );

        public IList ItemsSource
        {
            get => GetValue(ItemsSourceProperty) as IList;
            set => SetValue(ItemsSourceProperty, value);
        }

        // DataTemplate
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                nameof(ItemTemplate),
                typeof(DataTemplate),
                typeof(ScrollButtonsExt),
                null,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ScrollButtonsExt;
                    self.InitItems();
                }
            );
        public DataTemplate ItemTemplate
        {
            get => GetValue(ItemTemplateProperty) as DataTemplate;
            set => SetValue(ItemTemplateProperty, value);
        }

        // Selected item
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(ScrollButtonsExt),
                null,
                BindingMode.TwoWay,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ScrollButtonsExt;
                    self.SelectedChanged();
                }
            );
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        // SelectedIndex
        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(ScrollButtonsExt),
                -1,
                BindingMode.TwoWay,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ScrollButtonsExt;
                    self.SelectedChanged();
                }
            );
        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        // Selected color
        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create(
                nameof(SelectedColor),
                typeof(Color),
                typeof(ScrollButtonsExt),
                Color.Default
            );
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        // Unselected color
        public static readonly BindableProperty UnselectedColorProperty =
            BindableProperty.Create(
                nameof(UnselectedColor),
                typeof(Color),
                typeof(ScrollButtonsExt),
                Color.Default
            );
        public Color UnselectedColor
        {
            get => (Color)GetValue(UnselectedColorProperty);
            set => SetValue(UnselectedColorProperty, value);
        }

        // Порядок заполнения элементов в ширину
        public static readonly BindableProperty WidthRuleProperty =
            BindableProperty.Create(
                nameof(WidthRule),
                typeof(string),
                typeof(ScrollButtonsExt),
                null,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ScrollButtonsExt;
                    string old = o as string;
                    self.ParseWidthRule(old);
                    self.InvalidateLayout();
                }
            );
        public string WidthRule
        {
            get => GetValue(WidthRuleProperty) as string;
            set => SetValue(WidthRuleProperty, value);
        }

        // радиус углов 
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(
                nameof(CornerRadius),
                typeof(int),
                typeof(ScrollButtonsExt),
                0
            );

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // padding  
        //public static readonly BindableProperty PaddingProperty =
        //    BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(RadioButtonHost), new Thickness(),
        //        propertyChanged: (b, o, n) =>
        //        {
        //            var self = b as RadioButtonHost;
        //            self.InvalidateMeasure();
        //            self.InvalidateLayout();
        //        });
        //public Thickness Padding
        //{
        //    get => (Thickness)GetValue(PaddingProperty);
        //    set => SetValue(PaddingProperty, value);
        //}

        // раcстояние между элементами  
        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create(
                nameof(Spacing),
                typeof(double),
                typeof(ScrollButtonsExt),
                10.0,
                propertyChanged: (b, o, n) =>
                {
                    var self = b as ScrollButtonsExt;
                    self.InvalidateLayout();
                });

        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        // padding X
        public static readonly BindableProperty PaddingXProperty =
            BindableProperty.Create(
                nameof(PaddingX),
                typeof(int),
                typeof(ScrollButtonsExt),
                0);

        public int PaddingX
        {
            get => (int)GetValue(PaddingXProperty);
            set => SetValue(PaddingXProperty, value);
        }
        #endregion props


        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            // Работаем только по горизонтали
            if (Children.Count == 0)
                return;

            double startX = Padding.Left;
            double startY = Padding.Top;
            int index = 0;
            GridLength rule = default;
            bool isIgnoreWidthMeasure = false;

            foreach (var item in Children)
            {
                double w = 0;
                double h = 0;

                if (widthRule != null && index <= widthRule.Count - 1)
                {
                    var colRule = widthRule[index];
                    rule = widthRule[index].Width;
                    if (rule.IsAbsolute || rule.IsStar)
                        isIgnoreWidthMeasure = true;
                }

                var size = item.Measure(width, double.PositiveInfinity, MeasureFlags.IncludeMargins);
                if (isIgnoreWidthMeasure && Children.Count > 0)
                {
                    var parentWidth = width - Spacing * (Children.Count - 1);
                    w = CalcWidth(parentWidth, rule, widthRule, Children.Count);
                    h = size.Minimum.Height;
                    item.WidthRequest = w;
                }
                else
                {
                    w = size.Request.Width;
                    h = size.Minimum.Height;
                }

                var rect = new Rectangle(startX, startY, w, h);
                LayoutChildIntoBoundingRegion(item, rect);

                startX += w;

                // делаем учет расстояния между элементами
                if (index < Children.Count - 1)
                    startX += Spacing;

                index++;
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (Children.Count == 0)
                return new SizeRequest();

            widthConstraint -= Padding.HorizontalThickness;
            double maxHeight = 0;
            double maxWidth = 0;

            int count = 0;
            foreach (var item in Children)
            {
                count++;
                var size = item.Measure(widthConstraint, double.PositiveInfinity, MeasureFlags.IncludeMargins);
                double h = size.Minimum.Height;
                double w = size.Request.Width;

                // Высота
                if (h > maxHeight)
                    maxHeight = h;

                // Ширина
                maxWidth += w;

                if (count < Children.Count)
                    maxWidth += Spacing;
            }

            maxHeight += Padding.VerticalThickness;

            // защита от бесконечной ширины
            if (widthConstraint == double.PositiveInfinity)
                widthConstraint = maxWidth;

            return new SizeRequest(new Size(widthConstraint, maxHeight));
        }

        private void InitItems()
        {
            Children.Clear();

            if (ItemsSource != null)
                foreach (var itemContext in ItemsSource)
                {
                    CreateItem(itemContext);
                }
        }

        private void CreateItem(object context)
        {
            if (ItemTemplate == null)
                return;

            // Переработать в более грамотное и сокращённое условие

            View view = ItemTemplate.CreateContent() as View;

            if (view is ButtonExt button)
            {
                button.ColorTap = SelectedColor;
                button.CommandTap = commandTap;
                button.CommandTapParametr = context;
            }
            else
            {
                Touch.SetColor(view, SelectedColor);
                Touch.SetTap(view, commandTap);
                Touch.SetTapParameter(view, context);
            }

            view.BindingContext = context;
            view.BackgroundColor = UnselectedColor;

            Children.Add(view);
        }

        private void ActionTap(object context)
        {
            if (ItemsSource == null)
                return;

            int index = ItemsSource.IndexOf(context);
            if (index < 0)
                return;

            SelectedIndex = index;
            SelectedItem = context;
        }

        private void SelectedChanged()
        {
            // Находим индекс для выбранного элемента
            int index = SelectedIndex;
            View view = null;
            if (Children.Count > 0 && index >= 0 && index <= Children.Count - 1)
                view = Children[index];

            // Меняем цвет выделения
            if (lastTapItem != null && lastTapItem != view)
                lastTapItem.BackgroundColor = UnselectedColor;

            if (view != null)
                view.BackgroundColor = SelectedColor;

            lastTapItem = view;
        }

        private void ParseWidthRule(string old)
        {
            var newRule = WidthRule;
            if (string.IsNullOrWhiteSpace(newRule))
            {
                widthRule = null;
                return;
            }
            else if (newRule == old)
            {
                return;
            }

            widthRule = new ColumnDefinitionCollection();

            var rulls = newRule.Split(',');
            foreach (var item in rulls)
            {
                string s = item.Trim().ToLower();
                if (s == "auto")
                {
                    widthRule.Add(new ColumnDefinition() { Width = GridLength.Auto });
                }
                else if (s == "*")
                {
                    widthRule.Add(new ColumnDefinition() { Width = GridLength.Star });
                }
                else
                {
                    GridUnitType type;
                    string valueString;
                    int indexStar = s.IndexOf('*');

                    if (indexStar != -1)
                    {
                        valueString = s.Substring(0, indexStar);
                        type = GridUnitType.Star;
                    }
                    else
                    {
                        valueString = s;
                        type = GridUnitType.Absolute;
                    }

                    var cultureInfo = CultureInfo.GetCultureInfo("en-US");
                    if (double.TryParse(valueString, NumberStyles.Float, cultureInfo, out double value))
                    {
                        widthRule.Add(new ColumnDefinition()
                        {
                            Width = new GridLength(value, type)
                        });
                    }
                    else
                    {
                        throw new System.Exception("Ошибка формата правил GridLength");
                    }
                }
            }
        }

        private void RecalcRuleWidth()
        {
            int ruleCount = widthRule?.Count ?? 0;
            int itemsCount = ItemsSource?.Count ?? 0;

            if (ruleCount > 0 && ruleCount != itemsCount)
            {
                var lastRule = widthRule.FirstOrDefault().Width;
                while (widthRule.Count < itemsCount)
                {
                    widthRule.Add(new ColumnDefinition
                    {
                        Width = lastRule
                    });
                }
            }
        }

        private void OnSourceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InitItems();
        }

        internal static double CalcWidth(double parentWidth, GridLength col, ColumnDefinitionCollection cols, int itemsCount)
        {
            if (col.IsAbsolute)
            {
                return col.Value;
            }
            else
            {
                double com = 0;
                double dif = 0;

                for (int i = 0; i < itemsCount; i++)
                {
                    GridLength rule;
                    if (cols.Count > 0 && i <= cols.Count - 1)
                        rule = cols[i].Width;
                    else
                        rule = cols.LastOrDefault()?.Width ?? default;

                    if (rule.IsStar)
                        com += rule.Value;
                    else
                        dif += rule.Value;
                }

                //foreach (var c in cols)
                //{
                //    if (c.Width.IsStar)
                //        com += c.Width.Value;
                //    else
                //        dif += c.Width.Value;
                //}

                return (parentWidth - dif) * (col.Value / com);
            }
        }
    }
}