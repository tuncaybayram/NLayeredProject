using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entites.Concrete;

namespace Northwind.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _productService = new ProductManager(new EfProductDal());
            _categoryService = new CategoryManager(new EfCategoryDal());

        }
        private IProductService _productService;
        private ICategoryService _categoryService;

        private void Form1_Load(object sender, EventArgs e)
        {
            
            LoadCategories();
            LoadProducts();
            LoadCategories1();
        }

        private void LoadCategories1()
        {
            cbxCategoryId.DataSource = _categoryService.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryID";

        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryID";
            cbxCategoryIdUpdate.DataSource = _categoryService.GetAll();
            cbxCategoryIdUpdate.DisplayMember = "CategoryName";
            cbxCategoryIdUpdate.ValueMember = "CategoryID";
        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));

            }
            catch 
            {
                
            }
        }

        private void tbxProduct_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxProduct.Text))
            {
                dgwProduct.DataSource = _productService.GetProductByProductName(tbxProduct.Text);

            }
            else
            {
                LoadProducts();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productService.Add(new Product
            {
                CategoryID=Convert.ToInt32(cbxCategoryId.SelectedValue),
                ProductName=tbxProductName2.Text,
                QuantityPerUnit=tbxQuantityPerUnit.Text,
                UnitPrice=Convert.ToDecimal(tbxUnitPrice.Text),
                UnitsInStock=Convert.ToInt16(tbxStock.Text)
            });
            MessageBox.Show("Ürün Kaydedildi!");
            LoadProducts();
            
        }

        private void cbxCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productService.Update(new Product
            {
                ProductID =Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                ProductName=tbxProductNameUpdate.Text,
                CategoryID=Convert.ToInt32(cbxCategoryIdUpdate.SelectedValue),
                UnitsInStock=Convert.ToInt16(tbxUnitInStockUpdate.Text),
                QuantityPerUnit = tbxQuantityPerUnitUpdate.Text,
                UnitPrice=Convert.ToDecimal(tbxUnitPriceUpdate.Text),


            });
            LoadProducts();
            MessageBox.Show("Ürün Güncellendi");

        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgwProduct.CurrentRow;
            tbxProductNameUpdate.Text=row.Cells[1].Value.ToString();
            cbxCategoryIdUpdate.SelectedValue = row.Cells[3].Value;
            tbxUnitPriceUpdate.Text=row.Cells[5].Value.ToString();
            tbxQuantityPerUnitUpdate.Text=row.Cells[4].Value.ToString();    
            tbxUnitInStockUpdate.Text=row.Cells[6].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _productService.Delete(new Product
            {
                ProductID = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value) 
            });
            LoadProducts();
            MessageBox.Show("Ürün Silindi");
        }
    }
}
