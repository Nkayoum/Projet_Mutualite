﻿using Mutualite.BLL;
using Mutualite.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mutualite.WinForm
{
    public partial class FrmReunionList : Form
    {
        private ReunionBLO reunionBLO;
        private MembreBLO membreBLO;
        public FrmReunionList()
        {
            InitializeComponent();
            guna2DataGridViewReunion.AutoGenerateColumns = false;
            reunionBLO = new ReunionBLO(ConfigurationManager.AppSettings["DbFolder"]);
            membreBLO = new MembreBLO(ConfigurationManager.AppSettings["DbFolder"]);
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            guna2PanelContainerReunion.Controls.Add(childForm);
            guna2PanelContainerReunion.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void loadData()
        {
            string value = guna2TxtSearch.Text.ToLower();
            var reunions = reunionBLO.GetBy
            (
                x =>
                x.NomReunion.ToLower().Contains(value) ||
                x.DescriptionTontine.ToLower().Contains(value)
            ).OrderBy(x => x.NomReunion).ToArray();
            guna2DataGridViewReunion.DataSource = null;
            guna2DataGridViewReunion.DataSource = reunions;
            lblligne.Text = $"{guna2DataGridViewReunion.RowCount} lignes";
            guna2DataGridViewReunion.ClearSelection();
        }

        private void guna2GradientBtnCreerReunion_Click(object sender, EventArgs e)
        {
            Form f = new FrmReunion(loadData);
            f.Show();
        }

        private void FrmReunionList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void guna2TxtSearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void guna2GradientBtnRafraichir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TxtSearch.Text))
                loadData();
            else
                guna2TxtSearch.Clear();
        }

        private void guna2GradientBtnModifierReunion_Click(object sender, EventArgs e)
        {
            if (guna2DataGridViewReunion.SelectedRows.Count > 0)
            {
                for (int i = 0; i < guna2DataGridViewReunion.SelectedRows.Count; i++)
                {
                    Form f = new FrmReunion
                    (
                        guna2DataGridViewReunion.SelectedRows[i].DataBoundItem as Reunion,
                        loadData
                    );
                    f.ShowDialog();
                }
            }
        }

        private void guna2GradientBtnSupprimerReunion_Click(object sender, EventArgs e)
        {
            if (guna2DataGridViewReunion.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this meeting?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < guna2DataGridViewReunion.SelectedRows.Count; i++)
                    {
                        reunionBLO.DeleteReunion(guna2DataGridViewReunion.SelectedRows[i].DataBoundItem as Reunion);
                    }
                    loadData();
                }
            }
        }

        private void guna2DataGridViewReunion_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2GradientBtnModifierReunion_Click(sender, e);
        }

        private void guna2DataGridViewReunion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2GradientBtnCreerReunion_Click_1(object sender, EventArgs e)
        {
            openChildForm( new FrmReunion(loadData));
           
        }

        private void guna2GradientBtnSupprimerReunion_Click_1(object sender, EventArgs e)
        {
            if (guna2DataGridViewReunion.SelectedRows.Count > 0)
            {
                if (
                    MessageBox.Show
                    (
                        "Do you really want to delete this meeting?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    ) == DialogResult.Yes
                )
                {
                    for (int i = 0; i < guna2DataGridViewReunion.SelectedRows.Count; i++)
                    {
                        reunionBLO.DeleteReunion(guna2DataGridViewReunion.SelectedRows[i].DataBoundItem as Reunion);
                    }
                    loadData();
                }
            }
        }

        private void guna2GradientBtnModifierReunion_Click_1(object sender, EventArgs e)
        {
            if (guna2DataGridViewReunion.SelectedRows.Count > 0)
            {
                for (int i = 0; i < guna2DataGridViewReunion.SelectedRows.Count; i++)
                {
                    Form f = new FrmReunion
                    (
                        guna2DataGridViewReunion.SelectedRows[i].DataBoundItem as Reunion,
                        loadData
                    );
                    f.ShowDialog();
                }
            }
        }

        private void guna2GradientBtnRafraichir_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TxtSearch.Text))
                loadData();
            else
                guna2TxtSearch.Clear();
        }

        private void guna2TxtSearch_TextChanged_1(object sender, EventArgs e)
        {
            loadData();
        }

        private void guna2TxtSearch_TextChanged_2(object sender, EventArgs e)
        {
            loadData();
        }

        private void guna2GradientBtnAjoutMembre_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmMembre());
        }

        private void guna2GradientBtnListeMembre_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmMembresListe());
        }

        private void guna2GradientBtnCycleCour_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmCycleList());
        }
    }
}
