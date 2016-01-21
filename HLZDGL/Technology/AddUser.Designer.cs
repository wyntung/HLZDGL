namespace Technology
{
    partial class AddUser
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
            this.comboBoxUserGrpName = new System.Windows.Forms.ComboBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.textBoxTelephone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePickerBirthday = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxSex = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxVerifyPsw = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUserPsw = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBarCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxUserGrpName
            // 
            this.comboBoxUserGrpName.Enabled = false;
            this.comboBoxUserGrpName.Location = new System.Drawing.Point(109, 12);
            this.comboBoxUserGrpName.Name = "comboBoxUserGrpName";
            this.comboBoxUserGrpName.Size = new System.Drawing.Size(128, 20);
            this.comboBoxUserGrpName.TabIndex = 44;
            this.comboBoxUserGrpName.DropDown += new System.EventHandler(this.comboBoxUserGrpName_DropDown);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.Items.AddRange(new object[] {
            "在职",
            "离职"});
            this.comboBoxStatus.Location = new System.Drawing.Point(357, 12);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(88, 20);
            this.comboBoxStatus.TabIndex = 43;
            this.comboBoxStatus.Text = "在职";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(261, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 23);
            this.label10.TabIndex = 42;
            this.label10.Text = "状态：";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(21, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 23);
            this.label9.TabIndex = 41;
            this.label9.Text = "用户组名：";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(313, 331);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 40;
            this.button2.Text = "取消";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(161, 331);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 39;
            this.button1.Text = "确定";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(23, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 23);
            this.label8.TabIndex = 38;
            this.label8.Text = "备注";
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.Location = new System.Drawing.Point(109, 211);
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.Size = new System.Drawing.Size(336, 104);
            this.richTextBoxDescription.TabIndex = 37;
            this.richTextBoxDescription.Text = "";
            // 
            // textBoxTelephone
            // 
            this.textBoxTelephone.Location = new System.Drawing.Point(357, 130);
            this.textBoxTelephone.Name = "textBoxTelephone";
            this.textBoxTelephone.Size = new System.Drawing.Size(88, 21);
            this.textBoxTelephone.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(261, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 23);
            this.label7.TabIndex = 35;
            this.label7.Text = "电话：";
            // 
            // dateTimePickerBirthday
            // 
            this.dateTimePickerBirthday.Location = new System.Drawing.Point(109, 170);
            this.dateTimePickerBirthday.Name = "dateTimePickerBirthday";
            this.dateTimePickerBirthday.Size = new System.Drawing.Size(128, 21);
            this.dateTimePickerBirthday.TabIndex = 34;
            this.dateTimePickerBirthday.Value = new System.DateTime(1980, 1, 1, 1, 1, 0, 0);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(21, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 23);
            this.label6.TabIndex = 33;
            this.label6.Text = "生日：";
            // 
            // comboBoxSex
            // 
            this.comboBoxSex.Items.AddRange(new object[] {
            "男",
            "女"});
            this.comboBoxSex.Location = new System.Drawing.Point(109, 130);
            this.comboBoxSex.Name = "comboBoxSex";
            this.comboBoxSex.Size = new System.Drawing.Size(56, 20);
            this.comboBoxSex.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(21, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 23);
            this.label5.TabIndex = 31;
            this.label5.Text = "性别：";
            // 
            // textBoxVerifyPsw
            // 
            this.textBoxVerifyPsw.Location = new System.Drawing.Point(357, 88);
            this.textBoxVerifyPsw.Name = "textBoxVerifyPsw";
            this.textBoxVerifyPsw.PasswordChar = '*';
            this.textBoxVerifyPsw.Size = new System.Drawing.Size(88, 21);
            this.textBoxVerifyPsw.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(261, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 23);
            this.label4.TabIndex = 29;
            this.label4.Text = "确认密码：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(21, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 23);
            this.label3.TabIndex = 28;
            this.label3.Text = "用户密码：";
            // 
            // textBoxUserPsw
            // 
            this.textBoxUserPsw.Location = new System.Drawing.Point(109, 88);
            this.textBoxUserPsw.Name = "textBoxUserPsw";
            this.textBoxUserPsw.PasswordChar = '*';
            this.textBoxUserPsw.Size = new System.Drawing.Size(128, 21);
            this.textBoxUserPsw.TabIndex = 27;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(357, 50);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(88, 21);
            this.textBoxUserName.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(261, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 25;
            this.label2.Text = "用户姓名：";
            // 
            // textBoxBarCode
            // 
            this.textBoxBarCode.Enabled = false;
            this.textBoxBarCode.Location = new System.Drawing.Point(109, 50);
            this.textBoxBarCode.MaxLength = 7;
            this.textBoxBarCode.Name = "textBoxBarCode";
            this.textBoxBarCode.Size = new System.Drawing.Size(128, 21);
            this.textBoxBarCode.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 23;
            this.label1.Text = "用户条码：";
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 377);
            this.Controls.Add(this.comboBoxUserGrpName);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.richTextBoxDescription);
            this.Controls.Add(this.textBoxTelephone);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePickerBirthday);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxSex);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxVerifyPsw);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxUserPsw);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxBarCode);
            this.Controls.Add(this.label1);
            this.Name = "AddUser";
            this.Text = "AddUser";
            this.Load += new System.EventHandler(this.AddUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxUserGrpName;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox richTextBoxDescription;
        private System.Windows.Forms.TextBox textBoxTelephone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePickerBirthday;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxSex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxVerifyPsw;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUserPsw;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBarCode;
        private System.Windows.Forms.Label label1;
    }
}