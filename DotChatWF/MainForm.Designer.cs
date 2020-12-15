namespace DotChatWF
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      this.listMessages = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.fieldUsername = new System.Windows.Forms.TextBox();
      this.fieldMessage = new System.Windows.Forms.TextBox();
      this.btnSend = new System.Windows.Forms.Button();
      this.updateLoop = new System.Windows.Forms.Timer(this.components);
      this.btnAuth = new System.Windows.Forms.Button();
      this.btnReg = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // listMessages
      // 
      this.listMessages.FormattingEnabled = true;
      this.listMessages.Location = new System.Drawing.Point(9, 10);
      this.listMessages.Margin = new System.Windows.Forms.Padding(2);
      this.listMessages.Name = "listMessages";
      this.listMessages.Size = new System.Drawing.Size(583, 277);
      this.listMessages.TabIndex = 0;
      this.listMessages.SelectedIndexChanged += new System.EventHandler(this.ListMessages_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 306);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(55, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Username";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(9, 334);
      this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(50, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Message";
      // 
      // fieldUsername
      // 
      this.fieldUsername.AutoCompleteCustomSource.AddRange(new string[] {
            "You are not logged in"});
      this.fieldUsername.Location = new System.Drawing.Point(81, 304);
      this.fieldUsername.Margin = new System.Windows.Forms.Padding(2);
      this.fieldUsername.Name = "fieldUsername";
      this.fieldUsername.ReadOnly = true;
      this.fieldUsername.Size = new System.Drawing.Size(267, 20);
      this.fieldUsername.TabIndex = 3;
      this.fieldUsername.Text = "You are not logged in";
      this.fieldUsername.TextChanged += new System.EventHandler(this.FieldUsername_TextChanged);
      // 
      // fieldMessage
      // 
      this.fieldMessage.Location = new System.Drawing.Point(81, 332);
      this.fieldMessage.Margin = new System.Windows.Forms.Padding(2);
      this.fieldMessage.Multiline = true;
      this.fieldMessage.Name = "fieldMessage";
      this.fieldMessage.Size = new System.Drawing.Size(391, 34);
      this.fieldMessage.TabIndex = 4;
      this.fieldMessage.TextChanged += new System.EventHandler(this.FieldMessage_TextChanged);
      // 
      // btnSend
      // 
      this.btnSend.Location = new System.Drawing.Point(483, 304);
      this.btnSend.Margin = new System.Windows.Forms.Padding(2);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(108, 63);
      this.btnSend.TabIndex = 5;
      this.btnSend.Text = "SEND";
      this.btnSend.UseVisualStyleBackColor = true;
      this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
      // 
      // updateLoop
      // 
      this.updateLoop.Interval = 1000;
      this.updateLoop.Tick += new System.EventHandler(this.UpdateLoop_Tick);
      // 
      // btnAuth
      // 
      this.btnAuth.Location = new System.Drawing.Point(12, 374);
      this.btnAuth.Margin = new System.Windows.Forms.Padding(2);
      this.btnAuth.Name = "btnAuth";
      this.btnAuth.Size = new System.Drawing.Size(106, 23);
      this.btnAuth.TabIndex = 6;
      this.btnAuth.Text = "Authentification";
      this.btnAuth.UseVisualStyleBackColor = true;
      this.btnAuth.Click += new System.EventHandler(this.BtnAuth_Click);
      // 
      // btnReg
      // 
      this.btnReg.Location = new System.Drawing.Point(131, 374);
      this.btnReg.Margin = new System.Windows.Forms.Padding(2);
      this.btnReg.Name = "btnReg";
      this.btnReg.Size = new System.Drawing.Size(106, 23);
      this.btnReg.TabIndex = 7;
      this.btnReg.Text = "Registartion";
      this.btnReg.UseVisualStyleBackColor = true;
      this.btnReg.Click += new System.EventHandler(this.BtnReg_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(601, 404);
      this.Controls.Add(this.btnReg);
      this.Controls.Add(this.btnAuth);
      this.Controls.Add(this.btnSend);
      this.Controls.Add(this.fieldMessage);
      this.Controls.Add(this.fieldUsername);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.listMessages);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "MainForm";
      this.Text = "MainForm";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.VisibleChanged += new System.EventHandler(this.MainForm_VisibleChanged);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listMessages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fieldUsername;
        private System.Windows.Forms.TextBox fieldMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Timer updateLoop;
    private System.Windows.Forms.Button btnAuth;
    private System.Windows.Forms.Button btnReg;
  }
}

