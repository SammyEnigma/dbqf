﻿using dbqf.Configuration;
using dbqf.Criterion;
using dbqf.Display.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace dbqf.Display.Preset
{
    /// <summary>
    /// Encapsulates logic relating to how to display a field in the preset search control.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PresetPart<T> : INotifyPropertyChanged, IDisposable, IGetParameter
    {
        public PresetPart(FieldPath path)
        {
            Path = path;
            FullWidth = false;
        }

        public virtual void Dispose()
        {
            if (UIElement != null)
                UIElement.Dispose();
        }
        
        /// <summary>
        /// Gets or sets the field used for this control.
        /// </summary>
        public virtual FieldPath Path 
        {
            get { return _field; }
            protected set
            {
                if (value == _field)
                    return;
                _field = value;
                OnPropertyChanged("Path");
            }
        }
        protected FieldPath _field;

        /// <summary>
        /// Gets or sets the parameter builder to use with this control.
        /// </summary>
        public virtual ParameterBuilder Builder 
        {
            get { return _builder; } 
            set
            {
                if (value == _builder)
                    return;
                _builder = value;
                OnPropertyChanged("Builder");
            }
        }
        protected ParameterBuilder _builder;

        /// <summary>
        /// Gets the control to display on the preset control.  Type should be base control/widget type depending on the UI system in use.
        /// </summary>
        public virtual UIElement<T> UIElement 
        {
            get { return _control; }
            set
            {
                _control = value;
                OnPropertyChanged("UIElement");
            }
        }
        protected UIElement<T> _control;

        /// <summary>
        /// In the preset control there are 2 columns, the first for the label and the second for the control.
        /// Setting FullWidth = True will use both columns for the control.
        /// </summary>
        public virtual bool FullWidth 
        {
            get { return _fullWidth; }
            set
            {
                if (value == _fullWidth)
                    return;
                _fullWidth = value;
                OnPropertyChanged("FullWidth");
            }
        }
        protected bool _fullWidth;

        /// <summary>
        /// Using the control and builder, constructs the final parameter based on what the user has selected.
        /// </summary>
        /// <returns>The parameter or null if no parameter can be provided.</returns>
        public virtual IParameter GetParameter()
        {
            return Builder.Build(Path, UIElement.GetValues());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}