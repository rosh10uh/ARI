using ARI.EVOS.Infra;
using ARI.EVOS.Infra.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace ARI.EVOS.CompApp.Forms
{
    /// <summary>
    ///  The Multiple-Document Interface (MDI) is a specification that defines a user interface for applications 
    ///  that enable the user to work with more than one document at the same time under one parent form (window)
    /// </summary>
    public partial class MdiForm : Form
    {
        private bool _visibleStatusbar = true;
        private bool _visibleMenu = true;

        public bool VisibleStatusbar
        {
            get { return _visibleStatusbar; }
            set
            {
                _visibleStatusbar = value;
                statusBar.Visible = _visibleStatusbar;
                Invalidate(true);
            }
        }

        public bool VisibleMenu
        {
            get { return _visibleMenu; }
            set
            {
                _visibleMenu = value;
                menu.Visible = _visibleMenu;
                Invalidate(true);
            }
        }

        public MdiForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This event is used to load the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MdiForm_Load(object sender, EventArgs e)
        {
            var mdiChild = ActivatorUtilities.CreateInstance(ServiceLocator.InstanceProvider, typeof(SearchDealerForm));
            this.Text = BaseConstant.ApplicationTitle;
            ((Form)mdiChild).MdiParent = this;
            ((Form)mdiChild).StartPosition = FormStartPosition.CenterScreen;
            ((Form)mdiChild).Show();
        }

        /// <summary>
        /// This event is used to set date and time on statusbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTime_Tick(object sender, EventArgs e)
        {
            CheckStatusOfInsertKey();
            CheckStatusOfCapsLockKey();
            toolStripStatusDate.Text = DateTime.Now.ToString(BaseConstant.DateFormat);
            toolStripStatusTime.Text = DateTime.Now.ToString(BaseConstant.TimeFormat);
        }

        /// <summary>
        /// Checked Status of Insert Key
        /// </summary>
        private void CheckStatusOfInsertKey()
        {
            var isPressed = Keyboard.IsKeyToggled(Key.Insert);
            if (isPressed)
            {
                toolStripStatusINS.Enabled = true;
            }
            else
            {
                toolStripStatusINS.Enabled = false;
            }
        }
        /// <summary>
        /// Checked Status of CapsLock Key
        /// </summary>
        private void CheckStatusOfCapsLockKey()
        {
            var isPressed = Keyboard.IsKeyToggled(Key.CapsLock);
            if (isPressed)
            {
                toolStripStatusCAPS.Enabled = true;
            }
            else
            {
                toolStripStatusCAPS.Enabled = false;
            }
        }
    }
}
