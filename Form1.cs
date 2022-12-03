using System.Collections;
using EmployeeManagement.exception;
using EmployeeManagement.writer;

namespace EmployeeManagement
{
    public partial class Form1 : Form
    {
        private readonly EmployeesCollection employeesCollection = new();
        private Employee? copiedEmployee = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop)
                Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeToAddMode();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copy thông tin nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            copiedEmployee = employeesCollection.getEmployeeAtIndex(lstNhanVien.SelectedIndex);
            pasteToolStripMenuItem.Enabled = true;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (copiedEmployee == null) return;

            fillInInputs(copiedEmployee);
            MessageBox.Show("Paste thông tin nhân viên thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cSVFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeesCollection.isEmpty())
            {
                MessageBox.Show("Danh sách nhân viên trống. Không thể xuất", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new()
            {
                Title = "Export as CSV file",
                Filter = "CSV file|*.csv",
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "") return;


            CSVWriter writer = new();
            string[] propNames = handleEmployeePropNames();
            string[] values = handleEmployeeValuesCSV();

            writer.WriteFile(saveFileDialog.FileName, propNames, values);
            MessageBox.Show("Lưu file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void jSONFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeesCollection.isEmpty())
            {
                MessageBox.Show("Danh sách nhân viên trống. Không thể xuất", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new()
            {
                Title = "Export as JSON file",
                Filter = "JSON file|*.json",
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "") return;

            JSONWriter writer = new();
            string[] propNames = handleEmployeePropNames();
            Hashtable[] values = handleEmployeeValuesJSON();

            writer.WriteFile(saveFileDialog.FileName, propNames, values);
            MessageBox.Show("Lưu file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string[] handleEmployeePropNames()
        {
            return typeof(Employee).GetProperties().Select(p =>
            {
                string? propStr = p.ToString();
                return propStr.Substring(propStr.IndexOf(" ") + 1);
            }).ToArray();
        }

        private string[] handleEmployeeValuesCSV()
        {
            return employeesCollection.GetAllEmployees().Select(employee =>
            {
                return $"{employee.Name},{employee.Age},{employee.Address},{employee.Room},{employee.Position}";
            }).ToArray();
        }

        private Hashtable[] handleEmployeeValuesJSON()
        {
            List<Hashtable> list = new();
            employeesCollection.GetAllEmployees().ToList().ForEach(employee =>
            {
                Hashtable data = new Hashtable
                {
                    { "Name", employee.Name },
                    { "Age", employee.Age },
                    { "Address", employee.Address },
                    { "Room", employee.Room },
                    { "Position", employee.Position }
                };
                list.Add(data);
            });
            return list.ToArray();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtHoTen.Text;
                Age age = txtTuoi.Text;
                string address = txtDiaChi.Text;
                string room = cbbPhong.Text;
                string position = cbbChucVu.Text;

                addEmployeeToList(new Employee(name, age, address, room, position));
            } catch (InvalidAgeException)
            {
                MessageBox.Show("Tuổi không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (EmployeeLackInformationException)
            {
                MessageBox.Show("Không thể thêm nhân viên. Vui lòng điền hết thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (EmployeeNotValidException)
            {
                MessageBox.Show("Nhân viên không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            deleteEmployeeFromList(lstNhanVien.SelectedIndex);
            changeToAddMode();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtHoTen.Text;
                Age age = txtTuoi.Text;
                string address = txtDiaChi.Text;
                string room = cbbPhong.Text;
                string position = cbbChucVu.Text;

                Employee modified = new(name, age, address, room, position);
                updateEmployeeInfo(lstNhanVien.SelectedIndex, modified);
            }
            catch (InvalidAgeException)
            {
                MessageBox.Show("Tuổi không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch (EmployeeLackInformationException)
            {
                MessageBox.Show("Không thể thêm nhân viên. Vui lòng điền hết thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (EmployeeNotValidException)
            {
                MessageBox.Show("Nhân viên không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstNhanVien.SelectedIndex;

            if (index < 0 || index >= employeesCollection.GetAllEmployees().Length) return;
            Employee filtered = employeesCollection.getEmployeeAtIndex(index);
            changeToEditMode(filtered);
        }

        private void changeToEditMode(Employee employee)
        {
            setLabelsEmployeeInfo(employee);
            setInputsEmployeeInfo(employee);
            showEditButtonsAndHideAddButtons();
            copyToolStripMenuItem.Enabled = true;
        }

        private void changeToAddMode()
        {
            emptyInputs();
            emptyLabels();
            showAddButtonAndHideEditButtons();
            unfocusListBox();
            copyToolStripMenuItem.Enabled = false;
        }

        private void unfocusListBox()
        {
            lstNhanVien.SelectedItem = null;
        }

        private void showEditButtonsAndHideAddButtons()
        {
            btnThem.Visible = false;
            btnCapNhat.Visible = true;
            btnXoa.Visible = true;
        }

        private void showAddButtonAndHideEditButtons()
        {
            btnThem.Visible = true;
            btnCapNhat.Visible = false;
            btnXoa.Visible = false;
        }

        private void addEmployeeToList(Employee employee)
        {
            if (employee == null)
                throw new EmployeeNotValidException();
            if (employee.Name == "" || employee.Address == "" || employee.Room == "" || employee.Position == "")
                throw new EmployeeLackInformationException();
            
            employeesCollection.AddEmployee(employee);
            lstNhanVien.Items.Add(employee.Name);
            emptyInputs();
            MessageBox.Show("Thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteEmployeeFromList(int index)
        {
            employeesCollection.DeleteEmployeeAtIndex(index);
            lstNhanVien.Items.RemoveAt(index);
            emptyInputs();
            emptyLabels();
            MessageBox.Show("Xóa nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void updateEmployeeInfo(int index, Employee modified)
        {
            employeesCollection.updateEmployeeInfo(index, modified);
            lblHoTen.Text = modified.Name;
            lblTuoi.Text = modified.Age.ToString();
            lblDiaChi.Text = modified.Address;
            lblPhong.Text = modified.Room;
            lstNhanVien.Items[lstNhanVien.SelectedIndex] = modified.Name;
            MessageBox.Show("Cập nhật thông tin nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fillInInputs(Employee employee)
        {
            txtHoTen.Text = employee.Name;
            txtTuoi.Text = employee.Age.ToString();
            txtDiaChi.Text = employee.Address.ToString();
            cbbPhong.Text = employee.Room;
            cbbChucVu.Text = employee.Position;
        }
                
        private void emptyInputs()
        {
            txtHoTen.Text = "";
            txtTuoi.Text = "";
            txtDiaChi.Text = "";
            cbbPhong.Text = "";
            cbbChucVu.Text = "";
        }

        private void emptyLabels()
        {
            lblHoTen.Text = "";
            lblTuoi.Text = "";
            lblDiaChi.Text = "";
            lblPhong.Text = "";
        }

        private void setLabelsEmployeeInfo(Employee employee)
        {
            lblHoTen.Text = employee.Name;
            lblTuoi.Text = employee.Age.ToString();
            lblDiaChi.Text = employee.Address;
            lblPhong.Text = employee.Room;
            lblChucVu.Text = employee.Position;
        }

        private void setInputsEmployeeInfo(Employee employee)
        {
            txtHoTen.Text = employee.Name;
            txtTuoi.Text = employee.Age.ToString();
            txtDiaChi.Text = employee.Address;
            cbbPhong.Text = employee.Room;
            cbbChucVu.Text = employee.Position;
        }

        
    }
}