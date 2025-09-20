using SchoolProject.Assessment_and_Exams;
 
using SchoolProject.Assigning_Forms;
using SchoolProject.Assigning_Forms.Assign_Subjects_to_Grades;
using SchoolProject.Attendance;
using SchoolProject.Audit_Log;
using SchoolProject.Behaviours;
using SchoolProject.Classes;
using SchoolProject.Comparison;
using SchoolProject.Comparisons;
using SchoolProject.Comparisons.Trends;
using SchoolProject.Employees;
using SchoolProject.Enrollment_Management;
using SchoolProject.Fees_and_Payments;
 
using SchoolProject.Global_Classes;
using SchoolProject.Graduation;
using SchoolProject.Guardians;
using SchoolProject.People;
using SchoolProject.Positions;
using SchoolProject.Receipts;
using SchoolProject.Salary_Deduction;
using SchoolProject.School_Info;
using SchoolProject.Students;
using SchoolProject.Subjects;
using SchoolProject.Teachers;
using SchoolProject.Terms;
using SchoolProject.Users;
using SchoolProjectBusiness;
 
using System.Linq;
using System.Text;
 
using System.Windows.Forms;
 
namespace SchoolProject
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        //private Timer notificationTimer;
        private bool notificationShown = false;
        public frmMain(frmLogin frmLogin)
        {
            InitializeComponent(); _frmLogin = frmLogin;
            lblUsername.Text = $"Welcome, {clsGlobal.CurrentUser.UserName}";
        }

        private void peopleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPeopleManage frmPeopleManage = new frmPeopleManage();
            frmPeopleManage.ShowDialog();
        }

        private void employeesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEmployeelist frmEmp = new frmEmployeelist();
            frmEmp.ShowDialog();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmManageUsers frmManageUsers = new frmManageUsers();
            frmManageUsers.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int currentuser = clsGlobal.CurrentUser.UserID;
            frmUserInfo frmUserInfo = new frmUserInfo(currentuser);
            frmUserInfo.ShowDialog();
        }

        private void changePasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int currentuser = clsGlobal.CurrentUser.UserID;
            frmChangePassword frmChangePassword = new frmChangePassword(currentuser);
            frmChangePassword.ShowDialog();
        }

        private void changeSchoolInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSchoolInfo frm = new frmSchoolInfo();
            frm.ShowDialog();
        }

        private void auditLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAuditLog frmAuditLog = new frmAuditLog();
            frmAuditLog.ShowDialog();
        }

        private void teachersLsitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTeacherList frm = new frmTeacherList();
            frm.ShowDialog();
        }

        private void studentsListToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmStudentList frmStudentList = new frmStudentList();
            frmStudentList.ShowDialog();
        }

        private void guardianListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardianList guardianList = new GuardianList();
            guardianList.ShowDialog();
        }

        private void employeesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeelist frmEmployeelist = new frmEmployeelist();
            frmEmployeelist.ShowDialog();
        }

        private void positionsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPositionManage frmPositionManage = new frmPositionManage();
            frmPositionManage.ShowDialog();
        }

        private void peopleListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeopleManage frmPeopleManage = new frmPeopleManage();
            frmPeopleManage.ShowDialog();
        }

        private void gradesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGradeslist frmGradeslist = new frmGradeslist();
            frmGradeslist.ShowDialog();
        }

        private void assignSubjectsToClassesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAssignSubjectsToGrades frm = new frmAssignSubjectsToGrades();
            frm.ShowDialog();
        }

        private void classesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClasseslist frmClasseslist = new frmClasseslist();
            frmClasseslist.ShowDialog();
        }

        private void subjectsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubjectList_cs frmSubjectList = new frmSubjectList_cs();
            frmSubjectList.ShowDialog();
        }

        private void oNewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAttendaceManagement frmAttendaceManagement = new frmAttendaceManagement();
            frmAttendaceManagement.ShowDialog();
        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSalaryDeductionSummary frmSalaryDeductionSummary = new frmSalaryDeductionSummary();
            frmSalaryDeductionSummary.ShowDialog();
        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPaymentManage frmPaymentManage = new frmPaymentManage();
            frmPaymentManage.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeesPayroll frmEmployeesPayroll = new frmEmployeesPayroll();
            frmEmployeesPayroll.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmfinancialdashboard frmfinancialdashboard = new frmfinancialdashboard();
            frmfinancialdashboard.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmTermslist frmTermslist = new frmTermslist();
            frmTermslist.ShowDialog();
        }

        private void studentResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GradeClassStudentsExam gradeClassStudentsExam = new GradeClassStudentsExam();
            gradeClassStudentsExam.ShowDialog();
        }

        private void graduateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGraduateStudents frmGraduateStudents = new frmGraduateStudents();
            frmGraduateStudents.ShowDialog();
        }

        private void feeManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCompareGradesScores frmCompare = new frmCompareGradesScores();
            frmCompare.ShowDialog();
        }

        private void compareClassesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCompareStudentsInAClass frmCompareStudents = new frmCompareStudentsInAClass();
            frmCompareStudents.ShowDialog();
        }

        private void behavioursListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmStudentBehaviorslist frmStudentBehaviorslist = new frmStudentBehaviorslist();
            //frmStudentBehaviorslist.ShowDialog();
        }

        private void enrollmnetsDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnrollmnetsDashboard enrollmnetsDashboard = new EnrollmnetsDashboard();
            enrollmnetsDashboard.ShowDialog();
        }

        private void monthlyEmployeeAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonthlyAttendanceView month = new MonthlyAttendanceView("Employees");
            month.ShowDialog();
        }

        private void monthlyStudentsAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonthlyAttendanceView month = new MonthlyAttendanceView("Students");
            month.ShowDialog();
        }

        private void timeTableGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeTableGenerator timeTableGenerator = new TimeTableGenerator();
            timeTableGenerator.ShowDialog();
        }

        private void form1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waittt form1 = new waittt();
            form1.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    //public class frmNotification : Form
    //{
    //    private FlowLayoutPanel panel;
    //    private List<int> _tuitionFeeIDs;
    //    private Button btnClose;

    //    public frmNotification(List<int> tuitionFeeIDs)
    //    {
    //        _tuitionFeeIDs = tuitionFeeIDs;

    //        Text = "Installments Due Notification";
    //        Size = new Size(700, 450);
    //        StartPosition = FormStartPosition.CenterScreen;
    //        FormBorderStyle = FormBorderStyle.FixedToolWindow;
    //        BackColor = Color.White;
    //        KeyPreview = true; // Enable keyboard shortcuts

    //        panel = new FlowLayoutPanel
    //        {
    //            Dock = DockStyle.Top,
    //            AutoScroll = true,
    //            Padding = new Padding(10),
    //            WrapContents = false,
    //            FlowDirection = FlowDirection.TopDown,
    //            Height = 350
    //        };

    //        btnClose = new Button
    //        {
    //            Text = "Close",
    //            AutoSize = true,
    //            Anchor = AnchorStyles.Right,
    //            DialogResult = DialogResult.OK
    //        };
    //        btnClose.Click += (s, e) => this.Close();
    //        btnClose.TabIndex = 0;

    //        Controls.Add(panel);
    //        Controls.Add(btnClose);

    //        // Place close button correctly after panel
    //        btnClose.Top = panel.Bottom + 10;
    //        btnClose.Left = this.ClientSize.Width - btnClose.Width - 20;
    //    }

    //    // Load data after form is fully shown
    //    protected override void OnShown(EventArgs e)
    //    {
    //        base.OnShown(e);
    //        LoadDueInstallments();
    //    }

    //    private void LoadDueInstallments()
    //    {
    //        panel.Controls.Clear();

    //        foreach (int tuitionFeeID in _tuitionFeeIDs)
    //        {
    //            DataTable dt = clsInstallment.GetInstallmentSummaryByTuitionFeeID(tuitionFeeID);

    //            // Use LINQ to filter due and unpaid rows
    //            var dueRows = dt.AsEnumerable()
    //                            .Where(r => !Convert.ToBoolean(r["IsPaid"]) &&
    //                                        Convert.ToDateTime(r["DueDate"]) <= DateTime.Today)
    //                            .ToList();

    //            foreach (var row in dueRows)
    //            {
    //                int installmentID = Convert.ToInt32(row["InstallmentID"]);
    //                int installmentNumber = Convert.ToInt32(row["InstallmentNumber"]);
    //                DateTime dueDate = Convert.ToDateTime(row["DueDate"]);
    //                decimal amount = Convert.ToDecimal(row["Amount"]);
    //                string fullName = row["FullName"].ToString();

    //                FlowLayoutPanel container = new FlowLayoutPanel
    //                {
    //                    AutoSize = true,
    //                    FlowDirection = FlowDirection.LeftToRight,
    //                    Margin = new Padding(5)
    //                };

    //                Button btnPay = new Button
    //                {
    //                    Text = "Pay",
    //                    Tag = installmentID,
    //                    AutoSize = true
    //                };
    //                btnPay.Click += BtnPay_Click;

    //                Label lbl = new Label
    //                {
    //                    Text = $"Student: {fullName} | TuitionFeeID: {tuitionFeeID} | " +
    //                           $"Installment #{installmentNumber} | Due: {dueDate:dd/MM/yyyy} | Amount: {amount:0.00}",
    //                    AutoSize = true,
    //                    Padding = new Padding(5, 8, 5, 5)
    //                };

    //                container.Controls.Add(btnPay);
    //                container.Controls.Add(lbl);

    //                panel.Controls.Add(container);
    //            }
    //        }

    //        // Debug if panel is empty
    //        if (panel.Controls.Count == 0)
    //            MessageBox.Show("No due installments found.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //    }

    //    private void BtnPay_Click(object sender, EventArgs e)
    //    {
    //        if (sender is Button btn)
    //        {
    //            int installmentID = (int)btn.Tag;
    //            var payForm = new frmPayInstallment(installmentID);
    //            var result = payForm.ShowDialog();

    //            if (result == DialogResult.OK)
    //            {
    //                MessageBox.Show("Payment completed successfully.");
    //                LoadDueInstallments(); // Refresh the list after payment
    //            }
    //        }
    //    }

    //    // Allow Esc to close
    //    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    //    {
    //        if (keyData == Keys.Escape)
    //        {
    //            Close();
    //            return true;
    //        }
    //        return base.ProcessCmdKey(ref msg, keyData);
    //    }
    //}


}
