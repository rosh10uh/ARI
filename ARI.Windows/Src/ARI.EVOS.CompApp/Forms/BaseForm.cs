using ARI.EVOS.Common.Models;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace ARI.EVOS.CompApp.Forms
{
    /// <summary>
    /// Base class to set basic thing for child form
    /// </summary>
    public abstract class BaseForm : Form
    {
        private readonly ILogger _logger;
        protected readonly IMessage _message;

        private readonly List<MakeModel> _makes = new List<MakeModel>();

        protected BaseForm(ILogger logger, IMessage message)
        {
            _logger = logger;
            _message = message;
        }

        /// <summary>
        /// Display status bar message
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ShowStatusBarMessage(string message = null)
        {
            if (((MdiForm)(this.MdiParent)) != null)
            {
                ((MdiForm)(this.MdiParent)).toolStripStatusMessage.Text = message;
            }
        }

        /// <summary>
        /// Display error message into message box
        /// </summary>
        /// <param name="errorMessage"></param>
        protected virtual void ShowValidationMessage(string errorMessage)
        {
            ShowStatusBarMessage();
            _message.Show(errorMessage, BaseConstant.ValidationMessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Log Information
        /// </summary>
        /// <param name="message"></param>
        protected virtual void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        /// <summary>
        /// Log Warning
        /// </summary>
        /// <param name="message"></param>
        protected virtual void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        /// <summary>
        /// Check value of Dealer's required property 
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        protected virtual bool CheckEmptyProperty(Control ctrl)
        {
            if ((ctrl is TextBox || ctrl is ComboBox) && string.IsNullOrEmpty(ctrl.Text.Trim()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clear Dealer's detail
        /// </summary>
        /// <param name="p_ctrl"></param>
        protected virtual void ClearFormDetails(Control p_ctrl)
        {
            foreach (Control _objCtrl in p_ctrl.Controls)
            {
                if (_objCtrl is GroupBox || _objCtrl is Panel)
                    ClearFormDetails(_objCtrl);

                if (_objCtrl is TextBox || _objCtrl is ComboBox || _objCtrl is MaskedTextBox)
                {
                    _objCtrl.Text = "";
                }

                var ctrl = _objCtrl as CheckBox;
                if (ctrl != null)
                {
                    CheckBox ch = (CheckBox)_objCtrl;
                    ch.Checked = false;
                }

                var ctrl1 = _objCtrl as ListView;
                if (ctrl1 != null)
                {
                    ((ListView)_objCtrl).Items.Clear();
                }
            }
        }

        /// <summary>
        ///  Prevent flockering 
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }


        /// <summary>
        /// Apply common button style
        /// </summary>
        /// <param name="button"></param>
        /// <param name="e"></param>
        protected virtual void ApplyButtonStyle(Button button, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, button.ClientRectangle,
                                    System.Drawing.SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                                    System.Drawing.SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                                    System.Drawing.SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                                    System.Drawing.SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset);
        }

        /// <summary>
        /// Check empty date value
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dateMask"></param>
        /// <returns>bool</returns>
        protected virtual bool IsEmptyDateValue(string date, string dateMask)
        {
            string valueFormat = date.Replace("#", " ").TrimEnd(new char[] { ' ' });
            string dateFormat = dateMask.Replace("#", " ").TrimEnd(new char[] { ' ' });
            if (dateFormat == valueFormat)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Bind make based on selected country
        /// </summary>
        /// <param name="makes">List<MakeModel></param>
        /// <param name="cmbCountry">Pass ComboBox which is selected</param>
        /// <param name="cmbMake">Pass combobox which is to fill based on selected combobox</param>
        protected virtual void BindMakeByCountry(List<MakeModel> makes, ComboBox cmbCountry, ComboBox cmbMake)
        {
            _makes?.Clear();
            if (!string.IsNullOrEmpty(cmbCountry.SelectedValue.ToString()))
            {
                _makes?.AddRange(makes.Where(x => x.CountryCode == cmbCountry.SelectedValue.ToString()));
            }
            else
            {
                _makes?.AddRange(makes);
            }
            _makes?.Insert(0, new MakeModel { CountryCode = "", MakeCode = "", MakeDescription = "" });
            cmbMake.DataSource = _makes.ToList();
            cmbMake.ValueMember = "MakeCode";
            cmbMake.DisplayMember = "MakeDescription";
        }
    }
}

