#region Copyright
/*
 * ExamplePropertyBasedFileTypePdn4 file type
 * Copyright (C) 2013-2014 ComSquare AG, Switzerland
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published load
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

#region Description
/*
 * This Paint.NET plugin demonstrates the usage of all IndirectUI controls
 * available in Paint.NET 4.
 */
#endregion

namespace PropertyBasedEffects
{
    #region Usings

    using PaintDotNet;
    using PaintDotNet.Effects;
    using PaintDotNet.IndirectUI;
    using PaintDotNet.PropertySystem;
    using PaintDotNet.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    #endregion

    #region PluginSupportInfo

    // ======================================================================
    // PluginSupportInfo
    // ======================================================================
    public class PluginSupportInfo : IPluginSupportInfo
    {
        // ----------------------------------------------------------------------
        /// <summary>
        /// </summary>
        public string DisplayName
        {
            get
            {
                return ((AssemblyProductAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
            }
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// </summary>
        public Version Version
        {
            get
            {
                return base.GetType().Assembly.GetName().Version;
            }
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// </summary>
        public string Author
        {
            get
            {
                return "Martin Osieka";
            }
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// </summary>
        public string Copyright
        {
            get
            {
                return ((AssemblyCopyrightAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// </summary>
        public Uri WebsiteUri
        {
            get
            {
                return new Uri("https://forums.getpaint.net/");
            }
        }

    }

    // Must be in front of the effect plugin class
    [PluginSupportInfo(typeof(PluginSupportInfo))]

    #endregion

    #region Example PropertyBasedPdn4 Effect

    // To create an effect in the Menu->Adjustments add the following line in front of your class 
    // [EffectCategory(EffectCategory.Adjustment)]
    public sealed class PdnPropertiesAndControlTypes
        : PropertyBasedEffect
    {
        // ----------------------------------------------------------------------
        /// <summary>
        /// Defines an abstract name for the effect class
        /// </summary>
        private readonly Type ClassType = typeof(PdnPropertiesAndControlTypes);

        // ----------------------------------------------------------------------
        /// <summary>
        /// Defines a user friendly name used for menu and dialog caption
        /// </summary>
        private static string StaticName
        {
            get { return "Properties and ControlTypes"; }
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// Defines an image used for menu and dialog caption (may be null)
        /// </summary>
        private static Bitmap StaticImage
        {
            get { return Properties.Resources.EffectIcon; }
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// Defines the submenu name where the effect should be placed.
        /// Prefered is one of the SubmenuNames constants
        /// (SubmenuNames.Artistic, Blurs, Distort, Noise, Photo, Render, Stylize)
        /// (may be null)
        /// If the effect is an Adjustment then see the comment at the beginning of the class.
        /// </summary>
        private static string StaticSubmenuName
        {
            get { return "Advanced"; }
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// Constructs an ExamplePropertyBasedEffect instance
        /// </summary>
        public PdnPropertiesAndControlTypes()

            : base(StaticName, StaticImage, StaticSubmenuName, new EffectOptions() { Flags = EffectFlags.Configurable })

        {
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// Destroys the ExamplePropertyBasedEffect instance
        /// Remove me. Only needed for special cases. 
        /// If resources must be freed then the prefered way is to override OnDispose()
        /// </summary>
        ~PdnPropertiesAndControlTypes()
        {
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// Identifiers of the properties used by the effect
        /// </summary>
        private enum PropertyNames
        {
            BooleanProperty,
            DoubleProperty,
            DoubleProperty_AngleChooser,
            DoubleVectorProperty,
            DoubleVectorProperty_Slider,
            DoubleVector3Property,
            DoubleVector3Property_RollBallAndSliders,
            Int32Property,
            Int32Property_ColorWheel,
            Int32Property_IncrementButton,
            StaticListChoiceProperty,
            StaticListChoiceProperty_RadioButton,
            StringProperty,
            StringProperty_FileChooser,
            UriProperty,
        }

        /// <summary>
        /// Identifiers used in a dropdown choice property
        /// </summary>
        private enum ListItemsType
        {
            ListItem1,
            ListItem2,
            ListItem3
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// Configure the properties of the effect.
        /// This just creates the properties not the controls used in the dialog.
        /// These properties are defining the content of the EffectToken.
        /// </summary>
        protected override PropertyCollection OnCreatePropertyCollection()
        {
            // Add properties of all types and control types (always the variant with minimal parameters)

            List<Property> props = new()
            {
                new BooleanProperty(PropertyNames.BooleanProperty),     // Default: PropertyControlType.CheckBox
                new DoubleProperty(PropertyNames.DoubleProperty),       // Default: PropertyControlType.Slider
                new DoubleProperty(PropertyNames.DoubleProperty_AngleChooser, 45, 0, 360),
                new DoubleVectorProperty(PropertyNames.DoubleVectorProperty_Slider,
                    Pair.Create(0.0, 0.0),
                    Pair.Create(-1.0, -1.0),
                    Pair.Create(1.0, 1.0)),

                new DoubleVectorProperty(PropertyNames.DoubleVectorProperty,   // Default: PropertyControlType.PanAndSlider
                    Pair.Create(0.0, 0.0),
                    Pair.Create(-1.0, -1.0),
                    Pair.Create(1.0, 1.0)),
                new DoubleVector3Property(PropertyNames.DoubleVector3Property, // Default: PropertyControlType.Slider
                    Tuple.Create<double,double,double>(0, 0, 0),
                    Tuple.Create<double,double,double>(-1, -1, -1),
                    Tuple.Create<double,double,double>(1, 1, 1)),
                new DoubleVector3Property(PropertyNames.DoubleVector3Property_RollBallAndSliders,
                    Tuple.Create<double,double,double>(0, 0, 0),
                    Tuple.Create<double,double,double>(-180, -180, 0),
                    Tuple.Create<double,double,double>(180, 180, 90)),
                //ImageProperty (not implemented)
                new Int32Property(PropertyNames.Int32Property),         // Default: PropertyControlType.Slider
                new Int32Property(PropertyNames.Int32Property_ColorWheel),
                new Int32Property(PropertyNames.Int32Property_IncrementButton),
                //ScalarProperty (abstract)
                StaticListChoiceProperty.CreateForEnum<ListItemsType>(   // Default: PropertyControlType.DropDown
                    PropertyNames.StaticListChoiceProperty, ListItemsType.ListItem1, false),
                StaticListChoiceProperty.CreateForEnum<ListItemsType>(
                    PropertyNames.StaticListChoiceProperty_RadioButton, ListItemsType.ListItem1, false),
                new StringProperty(PropertyNames.StringProperty),       // Default: PropertyControlType.TextBox
                new StringProperty(PropertyNames.StringProperty_FileChooser),
                new UriProperty(PropertyNames.UriProperty, new Uri("https://forums.getpaint.net")),                    // is PropertyControlType.TextBox
                //VectorProperty (abstract)
                //Vector3Property (abstract)
            };
            // Add rules (this list may be empty or null)
            List<PropertyCollectionRule> propRules = new()
            {
                // Let the readonly state of the properties depend on the checkbox property
                new ReadOnlyBoundToBooleanRule(PropertyNames.DoubleProperty, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.DoubleProperty_AngleChooser, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.DoubleVectorProperty, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.DoubleVectorProperty_Slider, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.DoubleVector3Property, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.DoubleVector3Property_RollBallAndSliders, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.Int32Property, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.Int32Property_ColorWheel, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.Int32Property_IncrementButton, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.StaticListChoiceProperty, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.StaticListChoiceProperty_RadioButton, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.StringProperty, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.StringProperty_FileChooser, PropertyNames.BooleanProperty, false),
                new ReadOnlyBoundToBooleanRule(PropertyNames.UriProperty, PropertyNames.BooleanProperty, false),

                // Available rules:
                // LinkValuesBasedOnBooleanRule, PropertyCollectionRule, SoftMutuallyBoundMinMaxRule
                // ReadOnlyBoundToBooleanRule, ReadOnlyBoundToNameValuesRule, ReadOnlyBoundToValueRule
            };
            return new PropertyCollection(props, propRules);

        } /* OnCreatePropertyCollection */

        // ----------------------------------------------------------------------
        /// <summary>
        /// Configure the user interface of the effect.
        /// You may change the default control type of your properties or
        /// modify/suppress the default texts in the controls.
        /// PropertyControlType PDN3:
        ///   AngleChooser, CheckBox, ColorWheel, DropDown, IncrementButton,
        ///   PanAndSlider, RadioButton, Slider, TextBox, 
        /// PropertyControlType PDN4:
        ///   RollBallAndSliders,
        /// </summary>
        protected override ControlInfo OnCreateConfigUI(PropertyCollection props)
        {
            ControlInfo configUI = CreateDefaultConfigUI(props);
  
            //// Set control types
            // BooleanProperty (Default: PropertyControlType.CheckBox)
            // DoubleProperty (Default: PropertyControlType.Slider)
            configUI.SetPropertyControlType(PropertyNames.DoubleProperty_AngleChooser, PropertyControlType.AngleChooser);
  
            // DoubleVectorProperty (Default: PropertyControlType.PanAndSlider)
            configUI.SetPropertyControlType(PropertyNames.DoubleVectorProperty_Slider, PropertyControlType.Slider);
 
            // DoubleVector3Property (Default: PropertyControlType.Slider)
            configUI.SetPropertyControlType(PropertyNames.DoubleVector3Property_RollBallAndSliders, PropertyControlType.RollBallAndSliders);
  
            // Int32Property (Default: PropertyControlType.Slider)
            configUI.SetPropertyControlType(PropertyNames.Int32Property_ColorWheel, PropertyControlType.ColorWheel);
            configUI.SetPropertyControlType(PropertyNames.Int32Property_IncrementButton, PropertyControlType.IncrementButton);
 
            // StaticListChoiceProperty (Default: PropertyControlType.DropDown)
            configUI.SetPropertyControlType(PropertyNames.StaticListChoiceProperty_RadioButton, PropertyControlType.RadioButton);
 
            // StringProperty (Default: PropertyControlType.TextBox)
            configUI.SetPropertyControlType(PropertyNames.StringProperty_FileChooser, PropertyControlType.FileChooser);

            // UriProperty (Default: PropertyControlType.LinkLabel)
            //// Set control texts

            configUI.SetPropertyControlValue(PropertyNames.BooleanProperty, ControlInfoPropertyNames.DisplayName,
                "BooleanProperty  (Default: CheckBox)");

            configUI.SetPropertyControlValue(PropertyNames.BooleanProperty, ControlInfoPropertyNames.Description,
                "Set all following properties to ReadOnly");

            configUI.SetPropertyControlValue(PropertyNames.DoubleProperty, ControlInfoPropertyNames.DisplayName,
                "DoubleProperty  (Default: Slider)");

            configUI.SetPropertyControlValue(PropertyNames.DoubleProperty_AngleChooser, ControlInfoPropertyNames.DisplayName,
                "DoubleProperty  (AngleChooser)");

            configUI.SetPropertyControlValue(PropertyNames.DoubleVectorProperty, ControlInfoPropertyNames.DisplayName,
                "DoubleVectorProperty  (Default: PanAndSlider)");

            configUI.SetPropertyControlValue(PropertyNames.DoubleVectorProperty_Slider, ControlInfoPropertyNames.DisplayName,
                "DoubleVectorProperty  (Slider)");

            configUI.SetPropertyControlValue(PropertyNames.DoubleVector3Property, ControlInfoPropertyNames.DisplayName,
                "DoubleVector3Property  (Default: Slider)");

            configUI.SetPropertyControlValue(PropertyNames.DoubleVector3Property_RollBallAndSliders, ControlInfoPropertyNames.DisplayName,
                "DoubleVector3Property  (RollBallAndSliders)");

            configUI.SetPropertyControlValue(PropertyNames.Int32Property, ControlInfoPropertyNames.DisplayName,
                "Int32Property  (Default: Slider)");

            configUI.SetPropertyControlValue(PropertyNames.Int32Property_ColorWheel, ControlInfoPropertyNames.DisplayName,
                "Int32Property  (ColorWheel)");

            configUI.SetPropertyControlValue(PropertyNames.Int32Property_IncrementButton, ControlInfoPropertyNames.DisplayName,
                "Int32Property  (IncrementButton)");

            configUI.SetPropertyControlValue(PropertyNames.StaticListChoiceProperty, ControlInfoPropertyNames.DisplayName,
                "StaticListChoiceProperty  (Default: DropDown)");
 
            configUI.SetPropertyControlValue(PropertyNames.StaticListChoiceProperty_RadioButton, ControlInfoPropertyNames.DisplayName,
                "StaticListChoiceProperty  (RadioButton)");
 
            configUI.SetPropertyControlValue(PropertyNames.StringProperty, ControlInfoPropertyNames.DisplayName,
                "StringProperty  (Default: TextBox)");

            configUI.SetPropertyControlValue(PropertyNames.StringProperty_FileChooser, ControlInfoPropertyNames.DisplayName,
                "StringProperty  (FileChooser)");

            configUI.SetPropertyControlValue(PropertyNames.UriProperty, ControlInfoPropertyNames.DisplayName,
                "UriProperty  (Default: LinkLabel)");

            /*
 
            // Change individual properties (see ControlInfoPropertyNames)

            // Add the source surface to the pan control
            Bitmap bmp = this.EnvironmentParameters.SourceSurface.CreateAliasedBitmap();
            ImageResource ir = ImageResource.FromImage(bmp);
            configUI.SetPropertyControlValue(PropertyNames.DoubleVectorPanAndSlider, ControlInfoPropertyNames.StaticImageUnderlay, ir);
                
            // Set user friendly text to the radio buttons
            PropertyControlInfo pci = configUI.FindControlForPropertyName(PropertyNames.EnumRadioButtons);
            pci.SetValueDisplayName(RadioButtonEnum.RadioButton1, "Use color from color wheel");
            pci.SetValueDisplayName(RadioButtonEnum.RadioButton2, "Use primary color");
            pci.SetValueDisplayName(RadioButtonEnum.RadioButton3, "Use secondary color");
            
            // Description of the checkbox is used for the checkbox text!

            */
            return configUI;
        } /* OnCreateConfigUI */

        // ----------------------------------------------------------------------
        /// <summary>
        /// Configure the dialog of the effect.
        /// </summary>
        protected override void OnCustomizeConfigUIWindowProperties(PropertyCollection props)
        {

            props[ControlInfoPropertyNames.WindowTitle].Value = "Properties and ControlTypes (PDN 4.300)";

            props[ControlInfoPropertyNames.WindowIsSizable].Value = true;

            props[ControlInfoPropertyNames.WindowHelpContentType].Value = WindowHelpContentType.CustomViaCallback;


            base.OnCustomizeConfigUIWindowProperties(props);
        } /* OnCustomizeConfigUIWindowProperties */

        private void OnWindowHelpButtonClicked(IWin32Window owner, string helpContent)
        {
            MessageBox.Show(owner, ""
                + "This paint.net effect dialog shows all valid combinations of properties and control types. "
                + "paint.net allows to fine tune these properties even further but that's part of your job ;-)",
                "Help - Properties and ControlTypes", MessageBoxButtons.OK, MessageBoxIcon.None);

            //var form = Form.FromHandle(owner.Handle);
            //form.Capture = true;
            //form.Cursor = Cursors.Help;      
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// Called after the token of the effect changed.
        /// This method is used to read all values of the token to instance variables.
        /// These instance variables are then used to render the surface.
        /// </summary>
        protected override void OnSetRenderInfo(PropertyBasedEffectConfigToken effectToken, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            /*
            // Read the current settings of the properties
            propEnumDropDown = (DropDownEnum)effectToken.GetProperty<StaticListChoiceProperty>(PropertyNames.EnumDropDown).Value;
            propEnumRadioButtons = (RadioButtonEnum)effectToken.GetProperty<StaticListChoiceProperty>(PropertyNames.EnumRadioButtons).Value;
            propBooleanCheckBox = effectToken.GetProperty<BooleanProperty>(PropertyNames.BooleanProperty).Value;
            propStringTextBox = effectToken.GetProperty<StringProperty>(PropertyNames.StringProperty).Value;
            propInt32Slider = effectToken.GetProperty<Int32Property>(PropertyNames.Int32Slider).Value;
            propInt32IncrementButton = effectToken.GetProperty<Int32Property>(PropertyNames.Int32IncrementButton).Value;
            propInt32ColorWheel = effectToken.GetProperty<Int32Property>(PropertyNames.Int32ColorWheel).Value;
            propDoubleSlider = effectToken.GetProperty<DoubleProperty>(PropertyNames.DoubleSlider).Value;
            propDoubleAngleChooser = effectToken.GetProperty<DoubleProperty>(PropertyNames.DoubleAngleChooser).Value;
            propDoubleVectorSlider = effectToken.GetProperty<DoubleVectorProperty>(PropertyNames.DoubleVectorSlider).Value;
            propDoubleVectorPanAndSlider = effectToken.GetProperty<DoubleVectorProperty>(PropertyNames.DoubleVectorPanAndSlider).Value;
            propDoubleVector3Slider = effectToken.GetProperty<DoubleVector3Property>(PropertyNames.DoubleVector3Slider).Value;
            propDoubleVector3RollBallAndSliders = effectToken.GetProperty<DoubleVector3Property>(PropertyNames.DoubleVector3RollBallAndSliders).Value;


            envSelBounds = EnvironmentParameters.SelectionBounds;
            envSelCenterX = envSelBounds.Left + envSelBounds.Width / 2;
            envSelCenterY = envSelBounds.Top + envSelBounds.Height / 2;
            */
            System.Diagnostics.Trace.WriteLine("=== Property Values ===");

            var propBooleanProperty = effectToken.GetProperty<BooleanProperty>(PropertyNames.BooleanProperty).Value;
            System.Diagnostics.Trace.WriteLine("BooleanProperty (CheckBox) Boolean=" + propBooleanProperty);

            var propDoubleProperty = effectToken.GetProperty<DoubleProperty>(PropertyNames.DoubleProperty).Value;
            System.Diagnostics.Trace.WriteLine("DoubleProperty (Slider) Double=" + propDoubleProperty);

            var propDoubleProperty_AngleChooser = effectToken.GetProperty<DoubleProperty>(PropertyNames.DoubleProperty_AngleChooser).Value;
            System.Diagnostics.Trace.WriteLine("DoubleProperty (AngleChooser) Double=" + propDoubleProperty_AngleChooser);

            var propDoubleVectorProperty_Slider = effectToken.GetProperty<DoubleVectorProperty>(PropertyNames.DoubleVectorProperty_Slider).Value;
            System.Diagnostics.Trace.WriteLine("DoubleVectorProperty (Slider) Pair<Double,Double>=(" + propDoubleVectorProperty_Slider.First + ", " + propDoubleVectorProperty_Slider.Second + ")");

            var propDoubleVectorProperty = effectToken.GetProperty<DoubleVectorProperty>(PropertyNames.DoubleVectorProperty).Value;
            System.Diagnostics.Trace.WriteLine("DoubleVectorProperty (PanAndSlider) Pair<Double,Double>=(" + propDoubleVectorProperty.First + ", " + propDoubleVectorProperty.Second + ")");

            var propDoubleVector3Property = effectToken.GetProperty<DoubleVector3Property>(PropertyNames.DoubleVector3Property).Value;
            System.Diagnostics.Trace.WriteLine("DoubleVector3Property (Slider) Tuple<Double,Double,Double>=" + propDoubleVector3Property);

            var propDoubleVector3Property_RollBallAndSliders = effectToken.GetProperty<DoubleVector3Property>(PropertyNames.DoubleVector3Property_RollBallAndSliders).Value;
            System.Diagnostics.Trace.WriteLine("DoubleVector3Property (RollBallAndSliders) Tuple<Double,Double,Double>=" + propDoubleVector3Property_RollBallAndSliders);

            var propInt32Property = effectToken.GetProperty<Int32Property>(PropertyNames.Int32Property).Value;
            System.Diagnostics.Trace.WriteLine("Int32Property (Slider) Int32=" + propInt32Property);

            var propInt32Property_ColorWheel = effectToken.GetProperty<Int32Property>(PropertyNames.Int32Property_ColorWheel).Value;
            System.Diagnostics.Trace.WriteLine("Int32Property (ColorWheel) Int32=" + propInt32Property_ColorWheel);

            var propInt32Property_IncrementButton = effectToken.GetProperty<Int32Property>(PropertyNames.Int32Property_IncrementButton).Value;
            System.Diagnostics.Trace.WriteLine("Int32Property (IncrementButton) Int32=" + propInt32Property_IncrementButton);

            var propStaticListChoiceProperty = (ListItemsType)effectToken.GetProperty<StaticListChoiceProperty>(PropertyNames.StaticListChoiceProperty).Value;
            System.Diagnostics.Trace.WriteLine("StaticListChoiceProperty (DropDown) Enum=" + propStaticListChoiceProperty);

            var propStaticListChoiceProperty_RadioButton = (ListItemsType)effectToken.GetProperty<StaticListChoiceProperty>(PropertyNames.StaticListChoiceProperty_RadioButton).Value;
            System.Diagnostics.Trace.WriteLine("StaticListChoiceProperty (RadioButton) Enum=" + propStaticListChoiceProperty_RadioButton);

            var propStringProperty = effectToken.GetProperty<StringProperty>(PropertyNames.StringProperty).Value;
            System.Diagnostics.Trace.WriteLine("StringProperty (TextBox) String='" + propStringProperty + "'");

            var propStringProperty_FileChooser = effectToken.GetProperty<StringProperty>(PropertyNames.StringProperty_FileChooser).Value;
            System.Diagnostics.Trace.WriteLine("StringProperty (FileChooser) String='" + propStringProperty_FileChooser + "'");

            var propUriProperty = effectToken.GetProperty<UriProperty>(PropertyNames.UriProperty).Value;
            System.Diagnostics.Trace.WriteLine("UriProperty (LinkLabel) Uri='" + propUriProperty + "'");

            base.OnSetRenderInfo(effectToken, dstArgs, srcArgs);
        } /* OnSetRenderInfo */

        // ----------------------------------------------------------------------
        /// <summary>
        /// Render an area defined by a list of rectangles
        /// This function may be called multiple times to render the area of
        //  the selection on the active layer
        /// </summary>
        protected override void OnRender(Rectangle[] rois, int startIndex, int length)
        {
            for (int i = startIndex; i < startIndex + length; ++i)
            {
                RenderRectangle(DstArgs.Surface, SrcArgs.Surface, rois[i]);
            }
        }

        // ----------------------------------------------------------------------
        /// <summary>
        /// The function to render one rectangle of the surface
        /// </summary>
        private void RenderRectangle(Surface dst, Surface src, Rectangle renderRect)
        {
            // Add your render code here

            // Uncomment the basic example you like to see

            // Copy the original content of the active layer to the selection
            //dst.CopySurface(src,renderRect.Location,renderRect);

            // Fill the selection of the active layer with transparent white (0x00FFFFFF)
            dst.Fill(renderRect, ColorBgra.Transparent);

        }

    }

    #endregion
}
