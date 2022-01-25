
namespace QueryRunner
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
            this.labelFileName = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.labelTrusted = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.checkBoxTrusted = new System.Windows.Forms.CheckBox();
            this.comboBoxDatabase = new System.Windows.Forms.ComboBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelFileName
            // 
            this.labelFileName.AutoEllipsis = true;
            this.labelFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFileName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFileName.Location = new System.Drawing.Point(12, 9);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(433, 58);
            this.labelFileName.TabIndex = 0;
            this.labelFileName.Text = "Имя файла";
            this.labelFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(12, 90);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(44, 13);
            this.labelServer.TabIndex = 1;
            this.labelServer.Text = "Сервер";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(12, 210);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(72, 13);
            this.labelDatabase.TabIndex = 2;
            this.labelDatabase.Text = "База данных";
            // 
            // labelTrusted
            // 
            this.labelTrusted.AutoSize = true;
            this.labelTrusted.Location = new System.Drawing.Point(12, 119);
            this.labelTrusted.Name = "labelTrusted";
            this.labelTrusted.Size = new System.Drawing.Size(120, 13);
            this.labelTrusted.TabIndex = 3;
            this.labelTrusted.Text = "Авторизация Windows";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(12, 151);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(38, 13);
            this.labelLogin.TabIndex = 4;
            this.labelLogin.Text = "Логин";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(12, 179);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(45, 13);
            this.labelPassword.TabIndex = 5;
            this.labelPassword.Text = "Пароль";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(154, 87);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(291, 20);
            this.textBoxServer.TabIndex = 6;
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(154, 148);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(291, 20);
            this.textBoxLogin.TabIndex = 7;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(154, 176);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(291, 20);
            this.textBoxPassword.TabIndex = 8;
            // 
            // checkBoxTrusted
            // 
            this.checkBoxTrusted.AutoSize = true;
            this.checkBoxTrusted.Location = new System.Drawing.Point(154, 119);
            this.checkBoxTrusted.Name = "checkBoxTrusted";
            this.checkBoxTrusted.Size = new System.Drawing.Size(41, 17);
            this.checkBoxTrusted.TabIndex = 9;
            this.checkBoxTrusted.Text = "Да";
            this.checkBoxTrusted.UseVisualStyleBackColor = true;
            this.checkBoxTrusted.CheckedChanged += new System.EventHandler(this.checkBoxTrusted_CheckedChanged);
            // 
            // comboBoxDatabase
            // 
            this.comboBoxDatabase.FormattingEnabled = true;
            this.comboBoxDatabase.Location = new System.Drawing.Point(154, 207);
            this.comboBoxDatabase.Name = "comboBoxDatabase";
            this.comboBoxDatabase.Size = new System.Drawing.Size(291, 21);
            this.comboBoxDatabase.TabIndex = 10;
            this.comboBoxDatabase.DropDown += new System.EventHandler(this.comboBoxDatabase_DropDown);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(190, 252);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 11;
            this.buttonExecute.Text = "Выполнить";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 301);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.comboBoxDatabase);
            this.Controls.Add(this.checkBoxTrusted);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelTrusted);
            this.Controls.Add(this.labelDatabase);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.labelFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выполнить скрипт...";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.Label labelTrusted;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.CheckBox checkBoxTrusted;
        private System.Windows.Forms.ComboBox comboBoxDatabase;
        private System.Windows.Forms.Button buttonExecute;
    }
}

