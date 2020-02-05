using System.Drawing;

namespace ARI.EVOS.CompApp.Forms
{
    partial class SearchDealerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelDealer = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbCountryCode = new System.Windows.Forms.ComboBox();
            this.dgvSearchDealer = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.btnAddDealer = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbMake = new System.Windows.Forms.ComboBox();
            this.txtDealerId = new System.Windows.Forms.TextBox();
            this.lblMake = new System.Windows.Forms.Label();
            this.lblDealerId = new System.Windows.Forms.Label();
            this.lblCountryCode = new System.Windows.Forms.Label();
            this.panelDealer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDealer)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDealer
            // 
            this.panelDealer.Controls.Add(this.btnSearch);
            this.panelDealer.Controls.Add(this.cmbCountryCode);
            this.panelDealer.Controls.Add(this.dgvSearchDealer);
            this.panelDealer.Controls.Add(this.btnExit);
            this.panelDealer.Controls.Add(this.btnClearSelection);
            this.panelDealer.Controls.Add(this.btnAddDealer);
            this.panelDealer.Controls.Add(this.lblName);
            this.panelDealer.Controls.Add(this.txtName);
            this.panelDealer.Controls.Add(this.cmbMake);
            this.panelDealer.Controls.Add(this.txtDealerId);
            this.panelDealer.Controls.Add(this.lblMake);
            this.panelDealer.Controls.Add(this.lblDealerId);
            this.panelDealer.Controls.Add(this.lblCountryCode);
            this.panelDealer.Location = new System.Drawing.Point(21, 30);
            this.panelDealer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelDealer.Name = "panelDealer";
            this.panelDealer.Size = new System.Drawing.Size(870, 568);
            this.panelDealer.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(709, 34);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(113, 32);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            this.btnSearch.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonStyle);
            // 
            // cmbCountryCode
            // 
            this.cmbCountryCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountryCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbCountryCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbCountryCode.FormattingEnabled = true;
            this.cmbCountryCode.Location = new System.Drawing.Point(14, 52);
            this.cmbCountryCode.Name = "cmbCountryCode";
            this.cmbCountryCode.Size = new System.Drawing.Size(97, 21);
            this.cmbCountryCode.TabIndex = 13;
            this.cmbCountryCode.SelectedValueChanged += new System.EventHandler(this.CmbCountryCode_SelectedValueChanged);
            this.cmbCountryCode.KeyUp += NevigateWithEnterKey;
            // 
            // dgvSearchDealer
            // 
            this.dgvSearchDealer.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
            this.dgvSearchDealer.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            this.dgvSearchDealer.AllowUserToAddRows = false;
            this.dgvSearchDealer.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSearchDealer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchDealer.Location = new System.Drawing.Point(5, 99);
            this.dgvSearchDealer.MultiSelect = false;
            this.dgvSearchDealer.Name = "dgvSearchDealer";
            this.dgvSearchDealer.ReadOnly = true;
            this.dgvSearchDealer.Size = new System.Drawing.Size(858, 355);
            this.dgvSearchDealer.TabIndex = 9;
            this.dgvSearchDealer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchDealer.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvSearchDealer_Click);
            this.dgvSearchDealer.RowsAdded += DgvSearchDealer_RowsAdded;
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnExit.Location = new System.Drawing.Point(748, 477);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(113, 32);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            this.btnExit.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonStyle);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.FlatAppearance.BorderSize = 0;
            this.btnClearSelection.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClearSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClearSelection.Location = new System.Drawing.Point(631, 477);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(113, 32);
            this.btnClearSelection.TabIndex = 11;
            this.btnClearSelection.Text = "Clear Selection";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.BtnClearSelection_Click);
            this.btnClearSelection.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonStyle);
            // 
            // btnAddDealer
            // 
            this.btnAddDealer.FlatAppearance.BorderSize = 0;
            this.btnAddDealer.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAddDealer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDealer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddDealer.Location = new System.Drawing.Point(512, 477);
            this.btnAddDealer.Name = "btnAddDealer";
            this.btnAddDealer.Size = new System.Drawing.Size(113, 32);
            this.btnAddDealer.TabIndex = 10;
            this.btnAddDealer.Text = "Add Dealer";
            this.btnAddDealer.UseVisualStyleBackColor = true;
            this.btnAddDealer.Click += new System.EventHandler(this.BtnAddDealer_Click);
            this.btnAddDealer.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonStyle);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblName.Location = new System.Drawing.Point(306, 31);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 13);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "Name: ";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtName.Location = new System.Drawing.Point(309, 51);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(201, 20);
            this.txtName.TabIndex = 4;
            this.txtName.KeyUp += NevigateWithEnterKey;
            // 
            // cmbMake
            // 
            this.cmbMake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMake.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbMake.FormattingEnabled = true;
            this.cmbMake.Location = new System.Drawing.Point(127, 52);
            this.cmbMake.Name = "cmbMake";
            this.cmbMake.Size = new System.Drawing.Size(81, 21);
            this.cmbMake.KeyUp += NevigateWithEnterKey;
            this.cmbMake.TabIndex = 1;
            // 
            // txtDealerId
            // 
            this.txtDealerId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDealerId.Location = new System.Drawing.Point(223, 52);
            this.txtDealerId.Name = "txtDealerId";
            this.txtDealerId.Size = new System.Drawing.Size(73, 20);
            this.txtDealerId.KeyUp += NevigateWithEnterKey;
            this.txtDealerId.TabIndex = 2;
            // 
            // lblMake
            // 
            this.lblMake.AutoSize = true;
            this.lblMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMake.Location = new System.Drawing.Point(127, 31);
            this.lblMake.Name = "lblMake";
            this.lblMake.Size = new System.Drawing.Size(37, 13);
            this.lblMake.TabIndex = 3;
            this.lblMake.Text = "Make:";
            // 
            // lblDealerId
            // 
            this.lblDealerId.AutoSize = true;
            this.lblDealerId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDealerId.Location = new System.Drawing.Point(220, 31);
            this.lblDealerId.Name = "lblDealerId";
            this.lblDealerId.Size = new System.Drawing.Size(58, 13);
            this.lblDealerId.TabIndex = 6;
            this.lblDealerId.Text = "Dealer ID: ";
            // 
            // lblCountryCode
            // 
            this.lblCountryCode.AutoSize = true;
            this.lblCountryCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCountryCode.Location = new System.Drawing.Point(14, 31);
            this.lblCountryCode.Name = "lblCountryCode";
            this.lblCountryCode.Size = new System.Drawing.Size(77, 13);
            this.lblCountryCode.TabIndex = 0;
            this.lblCountryCode.Text = "Country Code :";
            // 
            // frmSearchDealer
            // 
            this.ClientSize = new System.Drawing.Size(900, 583);
            this.ControlBox = false;
            this.Controls.Add(this.panelDealer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchDealer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SearchDealerForm_Load);
            this.panelDealer.ResumeLayout(false);
            this.panelDealer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchDealer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Panel panelDealer;
        public System.Windows.Forms.Label lblCountryCode;
        public System.Windows.Forms.Label lblMake;
        public System.Windows.Forms.Label lblDealerId;
        public System.Windows.Forms.Label lblName;
        public System.Windows.Forms.ComboBox cmbCountryCode;
        public System.Windows.Forms.ComboBox cmbMake;
        public System.Windows.Forms.Button btnSearch;
        public System.Windows.Forms.Button btnAddDealer;
        public System.Windows.Forms.Button btnClearSelection;
        public System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.TextBox txtDealerId;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.DataGridView dgvSearchDealer;
    }
}