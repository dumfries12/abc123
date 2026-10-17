
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using bt3.BUS;

namespace bt3
{
    public partial class Form1 : Form
    {
        TextBox txtMaSV, txtHoTen, txtEmail, txtDienThoai, txtTuKhoa;
        ComboBox cboLop, cboTrangThai, cboLocLop;
        DateTimePicker dtpNgaySinh;
        RadioButton rdoNam, rdoNu;
        NumericUpDown numDiem, numDiemTu;
        Button butThem, butSua, butXoa, butLamMoi;
        Button butTimKiem, butHienThiTatCa;
        DataGridView dgvSinhVien;
        Label lblTongSo;
        ErrorProvider errorProvider;

        private readonly SinhVienBUS svBUS = new SinhVienBUS();
        private readonly LopHocBUS lopBUS = new LopHocBUS();

        private readonly Color xanhDam =
            Color.FromArgb(25, 70, 107);

        private readonly Color xanh =
            Color.FromArgb(36, 118, 173);

        private readonly Color nen =
            Color.FromArgb(244, 247, 250);

        private bool dangNapDuLieu = false;

        public Form1()
        {
            InitializeComponent();
            Controls.Clear();

            BuildUI();

            Load += Form1_Load;
            txtMaSV.Leave += txtMaSV_Leave;

            butThem.Click += butThem_Click;
            butSua.Click += butSua_Click;
            butXoa.Click += butXoa_Click;
            butLamMoi.Click += butLamMoi_Click;
            butTimKiem.Click += butTimKiem_Click;
            butHienThiTatCa.Click += butHienThiTatCa_Click;

            cboLop.SelectedIndexChanged +=
                cboLop_SelectedIndexChanged;
        }

        // =========================
        // 1. TAO GIAO DIEN
        // =========================

        private void BuildUI()
        {
            Text = "Ứng dụng quản lý sinh viên";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1150, 760);
            MinimumSize = new Size(1000, 680);
            BackColor = nen;
            Font = new Font("Segoe UI", 9F);
            AutoScaleMode = AutoScaleMode.Font;

            errorProvider = new ErrorProvider();
            errorProvider.ContainerControl = this;

            TableLayoutPanel main = new TableLayoutPanel();
            main.Dock = DockStyle.Fill;
            main.Padding = new Padding(12);
            main.ColumnCount = 1;
            main.RowCount = 5;

            main.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            main.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 42));

            main.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 45));

            main.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 255));

            main.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));

            main.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            Controls.Add(main);

            // Thanh tieu de
            Panel header = new Panel();
            header.Dock = DockStyle.Fill;
            header.BackColor = xanhDam;

            Label lblHeader = new Label();
            lblHeader.Text = "  Ứng dụng quản lý sinh viên";
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font(
                "Segoe UI", 12F, FontStyle.Bold);
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign =
                ContentAlignment.MiddleLeft;

            header.Controls.Add(lblHeader);
            main.Controls.Add(header, 0, 0);

            // Tieu de chinh
            Label lblTitle = new Label();
            lblTitle.Text = "QUẢN LÝ SINH VIÊN";
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.ForeColor = xanhDam;
            lblTitle.Font = new Font(
                "Segoe UI", 16F, FontStyle.Bold);
            lblTitle.TextAlign =
                ContentAlignment.MiddleLeft;

            main.Controls.Add(lblTitle, 0, 1);

            // Khu vuc thong tin
            GroupBox grpThongTin = new GroupBox();
            grpThongTin.Text = "Thông tin sinh viên";
            grpThongTin.Dock = DockStyle.Fill;
            grpThongTin.Padding = new Padding(10);
            grpThongTin.BackColor = Color.White;

            TableLayoutPanel info = new TableLayoutPanel();
            info.Dock = DockStyle.Fill;
            info.ColumnCount = 6;
            info.RowCount = 3;
            info.Padding = new Padding(4);

            info.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 100));
            info.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 34));
            info.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 85));
            info.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33));
            info.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 75));
            info.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 33));

            for (int i = 0; i < 3; i++)
            {
                info.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 33.33F));
            }

            grpThongTin.Controls.Add(info);
            main.Controls.Add(grpThongTin, 0, 2);

            txtMaSV = new TextBox();
            txtMaSV.Name = "txtMaSV";

            txtHoTen = new TextBox();
            txtHoTen.Name = "txtHoTen";

            txtEmail = new TextBox();
            txtEmail.Name = "txtEmail";

            txtDienThoai = new TextBox();
            txtDienThoai.Name = "txtDienThoai";

            dtpNgaySinh = new DateTimePicker();
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Dock = DockStyle.Fill;

            rdoNam = new RadioButton();
            rdoNam.Name = "rdoNam";
            rdoNam.Text = "Nam";
            rdoNam.Checked = true;
            rdoNam.AutoSize = true;

            rdoNu = new RadioButton();
            rdoNu.Name = "rdoNu";
            rdoNu.Text = "Nữ";
            rdoNu.AutoSize = true;

            FlowLayoutPanel gender = new FlowLayoutPanel();
            gender.Dock = DockStyle.Fill;
            gender.WrapContents = false;
            gender.Controls.Add(rdoNam);
            gender.Controls.Add(rdoNu);

            cboLop = new ComboBox();
            cboLop.Name = "cboLop";
            cboLop.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cboLop.Dock = DockStyle.Fill;

            numDiem = new NumericUpDown();
            numDiem.Name = "numDiem";
            numDiem.Minimum = 0;
            numDiem.Maximum = 10;
            numDiem.DecimalPlaces = 1;
            numDiem.Increment = 0.5M;
            numDiem.Dock = DockStyle.Fill;

            cboTrangThai = new ComboBox();
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cboTrangThai.Items.AddRange(new object[]
            {
                "Đang học", "Bảo lưu", "Đã tốt nghiệp"
            });
            cboTrangThai.SelectedIndex = 0;
            cboTrangThai.Dock = DockStyle.Fill;

            AddField(info, 0, 0, "Mã sinh viên *", txtMaSV);
            AddField(info, 1, 0, "Họ và tên *", txtHoTen);
            AddField(info, 2, 0, "Lớp học *", cboLop);

            AddField(info, 0, 1, "Ngày sinh *", dtpNgaySinh);
            AddField(info, 1, 1, "Giới tính", gender);
            AddField(info, 2, 1, "Điểm *", numDiem);

            AddField(info, 0, 2, "Email *", txtEmail);
            AddField(info, 1, 2, "Điện thoại *", txtDienThoai);
            AddField(info, 2, 2, "Trạng thái", cboTrangThai);

            // Cac nut chuc nang
            FlowLayoutPanel actions = new FlowLayoutPanel();
            actions.Dock = DockStyle.Fill;
            actions.FlowDirection =
                FlowDirection.RightToLeft;
            actions.WrapContents = false;
            actions.Padding = new Padding(0, 8, 0, 4);

            butLamMoi = MakeButton(
                "Làm mới", Color.FromArgb(100, 116, 139));
            butLamMoi.Name = "butLamMoi";

            butXoa = MakeButton(
                "Xóa", Color.FromArgb(216, 75, 85));
            butXoa.Name = "butXoa";
            butXoa.Enabled = false;

            butSua = MakeButton("Sửa", xanh);
            butSua.Name = "butSua";
            butSua.Enabled = false;

            butThem = MakeButton(
                "Thêm", Color.FromArgb(39, 134, 90));
            butThem.Name = "butThem";

            actions.Controls.Add(butLamMoi);
            actions.Controls.Add(butXoa);
            actions.Controls.Add(butSua);
            actions.Controls.Add(butThem);

            main.Controls.Add(actions, 0, 3);

            // Khu vuc danh sach
            TableLayoutPanel listPanel = new TableLayoutPanel();
            listPanel.Dock = DockStyle.Fill;
            listPanel.ColumnCount = 1;
            listPanel.RowCount = 2;

            listPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));
            listPanel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            listPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            main.Controls.Add(listPanel, 0, 4);

            // Thanh tim kiem
            FlowLayoutPanel searchPanel = new FlowLayoutPanel();
            searchPanel.Dock = DockStyle.Fill;
            searchPanel.WrapContents = false;
            searchPanel.AutoScroll = true;
            searchPanel.BackColor = Color.White;
            searchPanel.Padding = new Padding(5, 12, 5, 5);

            txtTuKhoa = new TextBox();
            txtTuKhoa.Name = "txtTuKhoa";
            txtTuKhoa.Width = 140;

            cboLocLop = new ComboBox();
            cboLocLop.Name = "cboLocLop";
            cboLocLop.Width = 120;
            cboLocLop.DropDownStyle =
                ComboBoxStyle.DropDownList;

            numDiemTu = new NumericUpDown();
            numDiemTu.Name = "numDiemTu";
            numDiemTu.Minimum = 0;
            numDiemTu.Maximum = 10;
            numDiemTu.DecimalPlaces = 1;
            numDiemTu.Width = 60;

            butTimKiem = MakeButton("Tìm kiếm", xanh);
            butTimKiem.Name = "butTimKiem";
            butTimKiem.Width = 90;

            butHienThiTatCa = MakeButton(
                "Hiển thị tất cả",
                Color.FromArgb(230, 238, 245));
            butHienThiTatCa.Name = "butHienThiTatCa";
            butHienThiTatCa.ForeColor = xanhDam;
            butHienThiTatCa.Width = 120;

            searchPanel.Controls.Add(MakeLabel("Từ khóa"));
            searchPanel.Controls.Add(txtTuKhoa);
            searchPanel.Controls.Add(MakeLabel("Lớp"));
            searchPanel.Controls.Add(cboLocLop);
            searchPanel.Controls.Add(MakeLabel("Điểm từ"));
            searchPanel.Controls.Add(numDiemTu);
            searchPanel.Controls.Add(butTimKiem);
            searchPanel.Controls.Add(butHienThiTatCa);

            listPanel.Controls.Add(searchPanel, 0, 0);

            // Bang sinh vien
            Panel gridPanel = new Panel();
            gridPanel.Dock = DockStyle.Fill;
            gridPanel.BackColor = Color.White;
            gridPanel.Padding = new Padding(5);

            Panel gridHeader = new Panel();
            gridHeader.Dock = DockStyle.Top;
            gridHeader.Height = 35;

            Label lblDanhSach = new Label();
            lblDanhSach.Text = "Danh sách sinh viên";
            lblDanhSach.Font = new Font(
                "Segoe UI", 10F, FontStyle.Bold);
            lblDanhSach.ForeColor = xanhDam;
            lblDanhSach.Dock = DockStyle.Left;
            lblDanhSach.Width = 250;
            lblDanhSach.TextAlign =
                ContentAlignment.MiddleLeft;

            lblTongSo = new Label();
            lblTongSo.Name = "lblTongSo";
            lblTongSo.Text = "Tổng số: 0 sinh viên";
            lblTongSo.Dock = DockStyle.Right;
            lblTongSo.Width = 220;
            lblTongSo.TextAlign =
                ContentAlignment.MiddleRight;

            gridHeader.Controls.Add(lblDanhSach);
            gridHeader.Controls.Add(lblTongSo);

            dgvSinhVien = new DataGridView();
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.Dock = DockStyle.Fill;
            dgvSinhVien.BackgroundColor = Color.White;
            dgvSinhVien.BorderStyle = BorderStyle.None;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.RowHeadersVisible = false;
            dgvSinhVien.EnableHeadersVisualStyles = false;
            dgvSinhVien.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(220, 236, 247);
            dgvSinhVien.ColumnHeadersDefaultCellStyle.ForeColor =
                xanhDam;
            dgvSinhVien.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvSinhVien.ColumnHeadersHeight = 35;
            dgvSinhVien.RowTemplate.Height = 30;
            dgvSinhVien.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 249, 252);

            string[] properties =
            {
                "MaSV", "HoTen", "NgaySinh", "GioiTinh",
                "Email", "SoDienThoai", "Diem", "MaLop", "TrangThai"
            };

            string[] titles =
            {
                "Mã SV", "Họ và tên", "Ngày sinh", "Giới tính",
                "Email", "Điện thoại", "Điểm", "Mã lớp", "Trạng thái"
            };

            for (int i = 0; i < properties.Length; i++)
            {
                dgvSinhVien.Columns.Add(properties[i], titles[i]);
                dgvSinhVien.Columns[i].DataPropertyName =
                    properties[i];
            }

            gridPanel.Controls.Add(dgvSinhVien);
            gridPanel.Controls.Add(gridHeader);
            listPanel.Controls.Add(gridPanel, 0, 1);

            txtMaSV.TabIndex = 0;
            txtHoTen.TabIndex = 1;
            cboLop.TabIndex = 2;
            dtpNgaySinh.TabIndex = 3;
            rdoNam.TabIndex = 4;
            rdoNu.TabIndex = 5;
            numDiem.TabIndex = 6;
            txtEmail.TabIndex = 7;
            txtDienThoai.TabIndex = 8;
            cboTrangThai.TabIndex = 9;
        }

        private void AddField(
            TableLayoutPanel table,
            int column,
            int row,
            string title,
            Control control)
        {
            Label label = new Label();
            label.Text = title;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.AutoEllipsis = true;

            control.Dock = DockStyle.Fill;
            control.Margin = new Padding(3, 7, 7, 7);

            table.Controls.Add(label, column * 2, row);
            table.Controls.Add(control, column * 2 + 1, row);
        }

        private Label MakeLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(5, 8, 5, 5)
            };
        }

        private Button MakeButton(string text, Color color)
        {
            return new Button
            {
                Text = text,
                Width = 85,
                Height = 34,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(
                    "Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(4, 0, 4, 0),
                Cursor = Cursors.Hand
            };
        }

        // =========================
        // 2. NAP DU LIEU
        // =========================

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dangNapDuLieu = true;

                var classes = lopBUS.GetAll().ToList();

                cboLop.DataSource = classes.ToList();
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";

                cboLocLop.DataSource = classes.ToList();
                cboLocLop.DisplayMember = "TenLop";
                cboLocLop.ValueMember = "MaLop";
                cboLocLop.SelectedIndex = -1;

                LoadStudents(svBUS.GetAll());

                dangNapDuLieu = false;
                ClearForm();
            }
            catch (Exception ex)
            {
                dangNapDuLieu = false;

                MessageBox.Show(
                    ex.Message,
                    "Lỗi tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadStudents(List<SinhVien> students)
        {
            dgvSinhVien.DataSource = null;
            dgvSinhVien.DataSource = students.ToList();

            lblTongSo.Text =
                "Tổng số: " + students.Count + " sinh viên";
        }

        // =========================
        // 3. LAM MOI FORM
        // =========================

        private void ClearForm()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtDienThoai.Clear();

            dtpNgaySinh.Value = DateTime.Today;
            rdoNam.Checked = true;
            numDiem.Value = 0;

            if (cboLop.Items.Count > 0)
                cboLop.SelectedIndex = 0;

            if (cboTrangThai.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;

            txtMaSV.Enabled = true;
            txtHoTen.Enabled = true;
            txtEmail.Enabled = true;
            txtDienThoai.Enabled = true;
            dtpNgaySinh.Enabled = true;
            rdoNam.Enabled = true;
            rdoNu.Enabled = true;
            cboLop.Enabled = true;
            cboTrangThai.Enabled = true;
            numDiem.Enabled = true;

            butThem.Enabled = true;
            butSua.Enabled = false;
            butXoa.Enabled = false;

            errorProvider.Clear();
            txtMaSV.Focus();
        }

        private void butLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtTuKhoa.Clear();
            numDiemTu.Value = 0;
            cboLocLop.SelectedIndex = -1;
            LoadStudents(svBUS.GetAll());
        }

        // =========================
        // 4. LAY DU LIEU TU FORM
        // =========================

        private SinhVien GetStudentFromForm()
        {
            if (cboLop.SelectedValue == null)
                throw new Exception("Vui lòng chọn lớp học.");

            return new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
                Email = txtEmail.Text.Trim(),
                SoDienThoai = txtDienThoai.Text.Trim(),
                Diem = (double)numDiem.Value,
                MaLop = cboLop.SelectedValue.ToString(),
                TrangThai = cboTrangThai.Text
            };
        }

        // =========================
        // 5. TIM SINH VIEN THEO MA
        // =========================

        private void txtMaSV_Leave(object sender, EventArgs e)
        {
            if (dangNapDuLieu)
                return;

            string ma = txtMaSV.Text.Trim();

            if (string.IsNullOrWhiteSpace(ma))
                return;

            SinhVien sv = svBUS.GetByMaSV(ma);

            if (sv == null)
            {
                txtHoTen.Clear();
                txtEmail.Clear();
                txtDienThoai.Clear();

                dtpNgaySinh.Value = DateTime.Today;
                rdoNam.Checked = true;
                numDiem.Value = 0;

                butThem.Enabled = true;
                butSua.Enabled = false;
                butXoa.Enabled = false;

                return;
            }

            txtHoTen.Text = sv.HoTen;
            txtEmail.Text = sv.Email;
            txtDienThoai.Text = sv.SoDienThoai;

            if (sv.NgaySinh <= DateTime.Today)
                dtpNgaySinh.Value = sv.NgaySinh;

            rdoNam.Checked = sv.GioiTinh == "Nam";
            rdoNu.Checked = sv.GioiTinh == "Nữ";

            cboLop.SelectedValue = sv.MaLop;

            numDiem.Value = Math.Max(
                numDiem.Minimum,
                Math.Min(numDiem.Maximum, (decimal)sv.Diem));

            cboTrangThai.SelectedItem = sv.TrangThai;

            butThem.Enabled = false;
            butSua.Enabled = true;
            butXoa.Enabled = true;
        }

        // =========================
        // 6. THEM SINH VIEN
        // =========================

        private void butThem_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            try
            {
                SinhVien sv = GetStudentFromForm();

                svBUS.Add(sv);

                LoadStudents(svBUS.GetAll());

                MessageBox.Show(
                    "Thêm sinh viên thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =========================
        // 7. SUA SINH VIEN
        // =========================

        private void butSua_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên.");
                txtMaSV.Focus();
                return;
            }

            if (MessageBox.Show(
                "Bạn có chắc muốn sửa sinh viên này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                SinhVien sv = GetStudentFromForm();

                svBUS.Update(sv);

                LoadStudents(svBUS.GetAll());

                MessageBox.Show("Cập nhật sinh viên thành công.");

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =========================
        // 8. XOA SINH VIEN
        // =========================

        private void butXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Bạn có chắc muốn xóa sinh viên này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                svBUS.Delete(txtMaSV.Text.Trim());

                LoadStudents(svBUS.GetAll());

                MessageBox.Show("Xóa sinh viên thành công.");

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =========================
        // 9. TIM KIEM SINH VIEN
        // =========================

        private void butTimKiem_Click(object sender, EventArgs e)
        {
            List<SinhVien> result =
                svBUS.Search(txtTuKhoa.Text);

            if (cboLocLop.SelectedValue != null)
            {
                string maLop =
                    cboLocLop.SelectedValue.ToString();

                result = result
                    .Where(s => s.MaLop == maLop)
                    .ToList();
            }

            decimal diemToiThieu = numDiemTu.Value;

            result = result
                .Where(s => (decimal)s.Diem >= diemToiThieu)
                .ToList();

            LoadStudents(result);
        }

        private void butHienThiTatCa_Click(
            object sender, EventArgs e)
        {
            txtTuKhoa.Clear();
            numDiemTu.Value = 0;
            cboLocLop.SelectedIndex = -1;

            LoadStudents(svBUS.GetAll());
        }

        // =========================
        // 10. LOC SINH VIEN THEO LOP
        // =========================

        private void cboLop_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (dangNapDuLieu)
                return;

            if (cboLop.SelectedValue == null ||
                !(cboLop.SelectedValue is string))
                return;

            string maLop = cboLop.SelectedValue.ToString();

            LoadStudents(svBUS.GetByMaLop(maLop));
        }
    }
}
