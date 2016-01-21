namespace HLZDGL
{
    partial class ProductionOrder
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.productionOrderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mESDataSet = new HLZDGL.MESDataSet();
            this.productionOrderTableAdapter = new HLZDGL.MESDataSetTableAdapters.ProductionOrderTableAdapter();
            this.OrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Specification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Workshop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackingNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Voltage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jingdu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productionOrderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "新建";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(112, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderID,
            this.OrderCode,
            this.Type,
            this.Specification,
            this.BarCode,
            this.Customer,
            this.Branch,
            this.Number,
            this.Workshop,
            this.ProductName,
            this.PackingNumber,
            this.Voltage,
            this.jingdu});
            this.dataGridView1.DataSource = this.productionOrderBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(734, 429);
            this.dataGridView1.TabIndex = 26;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(210, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 27;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 447);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 48);
            this.panel1.TabIndex = 28;
            // 
            // productionOrderBindingSource
            // 
            this.productionOrderBindingSource.DataMember = "ProductionOrder";
            this.productionOrderBindingSource.DataSource = this.mESDataSet;
            // 
            // mESDataSet
            // 
            this.mESDataSet.DataSetName = "MESDataSet";
            this.mESDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productionOrderTableAdapter
            // 
            this.productionOrderTableAdapter.ClearBeforeFill = true;
            // 
            // OrderID
            // 
            this.OrderID.DataPropertyName = "OrderID";
            this.OrderID.HeaderText = "订单编码";
            this.OrderID.Name = "OrderID";
            this.OrderID.ReadOnly = true;
            // 
            // OrderCode
            // 
            this.OrderCode.DataPropertyName = "OrderCode";
            this.OrderCode.FillWeight = 138.4615F;
            this.OrderCode.HeaderText = "订单号";
            this.OrderCode.Name = "OrderCode";
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.FillWeight = 105.0072F;
            this.Type.HeaderText = "型号";
            this.Type.Name = "Type";
            // 
            // Specification
            // 
            this.Specification.DataPropertyName = "Specification";
            this.Specification.FillWeight = 99.886F;
            this.Specification.HeaderText = "规格";
            this.Specification.Name = "Specification";
            // 
            // BarCode
            // 
            this.BarCode.DataPropertyName = "BarCode";
            this.BarCode.FillWeight = 137.5808F;
            this.BarCode.HeaderText = "底座条码";
            this.BarCode.Name = "BarCode";
            // 
            // Customer
            // 
            this.Customer.DataPropertyName = "Customer";
            this.Customer.FillWeight = 125.7417F;
            this.Customer.HeaderText = "客户名称";
            this.Customer.Name = "Customer";
            // 
            // Branch
            // 
            this.Branch.DataPropertyName = "Branch";
            this.Branch.FillWeight = 133.2887F;
            this.Branch.HeaderText = "分公司名称";
            this.Branch.Name = "Branch";
            // 
            // Number
            // 
            this.Number.DataPropertyName = "Number";
            this.Number.FillWeight = 72.18209F;
            this.Number.HeaderText = "数量";
            this.Number.Name = "Number";
            // 
            // Workshop
            // 
            this.Workshop.DataPropertyName = "Workshop";
            this.Workshop.FillWeight = 101.3419F;
            this.Workshop.HeaderText = "车间名称";
            this.Workshop.Name = "Workshop";
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.FillWeight = 94.44437F;
            this.ProductName.HeaderText = "产品名称";
            this.ProductName.Name = "ProductName";
            // 
            // PackingNumber
            // 
            this.PackingNumber.DataPropertyName = "PackingNumber";
            this.PackingNumber.FillWeight = 74.87398F;
            this.PackingNumber.HeaderText = "装箱数";
            this.PackingNumber.Name = "PackingNumber";
            // 
            // Voltage
            // 
            this.Voltage.DataPropertyName = "Voltage";
            this.Voltage.FillWeight = 58.98405F;
            this.Voltage.HeaderText = "电压";
            this.Voltage.Name = "Voltage";
            // 
            // jingdu
            // 
            this.jingdu.DataPropertyName = "jingdu";
            this.jingdu.FillWeight = 58.20769F;
            this.jingdu.HeaderText = "精度";
            this.jingdu.Name = "jingdu";
            // 
            // ProductionOrder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(758, 495);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ProductionOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductionOrder";
            this.Load += new System.EventHandler(this.ProductionOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.productionOrderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mESDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private MESDataSet mESDataSet;
        private System.Windows.Forms.BindingSource productionOrderBindingSource;
        private MESDataSetTableAdapters.ProductionOrderTableAdapter productionOrderTableAdapter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Specification;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Workshop;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackingNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Voltage;
        private System.Windows.Forms.DataGridViewTextBoxColumn jingdu;
    }
}