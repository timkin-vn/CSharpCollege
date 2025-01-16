using GraphEditor.Business.Services;
using GraphEditor.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor
{
    public partial class GraphEditorForm : Form
    {
        private GraphEditorService _service = new GraphEditorService();

        private bool _isMouseDown;

        public GraphEditorForm()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateButton.Checked = !CreateButton.Checked;
        }

        private void GraphEditorForm_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseDown = true;
            if (CreateButton.Checked)
            {
                _service.SetCreateMode(e.Location);
                CreateButton.Checked = false;
            }
        }

        private void GraphEditorForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                _service.SetNewLocation(e.Location);
                Refresh();
            }
        }

        private void GraphEditorForm_MouseUp(object sender, MouseEventArgs e)
        {
            _service.ResetMode();
            _isMouseDown = false;
            Refresh();
        }

        private void GraphEditorForm_Paint(object sender, PaintEventArgs e)
        {
            var painter = new Painter();
            painter.Paint(e.Graphics, _service, true);
        }
    }
}
