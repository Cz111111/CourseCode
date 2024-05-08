using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ruangou_5_1;

namespace ruangou_6_win
{
    public partial class Form1 : Form
    {
        private List<string> queryChoiceList;
        private OrderService orderService= new OrderService();
        private List<Good> goods;
        private List<Customer> customers;
        public Form1()
        {
            InitializeComponent();
            InitOrders();
           
            queryChoiceList = new List<string>() { "ID", "客户名", "商品名", "总价格大于" };
            comboBox1.DataSource = queryChoiceList;
            order_view.DataSource=orderService.QueryAll();
            
            


        }
        private void InitOrders()
        {
            goods=new List<Good>();
            customers = new List<Customer>();
            
            
            //商品
            Good pen = new Good(1, "pen", 3f);
            Good pencil = new Good(2, "pencil", 1f);
            Good eraser = new Good(3, "eraser", 5f);
            Good ruler = new Good(4, "ruler", 2f);

            //顾客
            Customer jack = new Customer(1, "Jack");
            Customer john = new Customer(2, "John");
            Customer joe = new Customer(3, "Joe");

            //订单
            Order order1 = new Order(1, jack);
            order1.AddDetails(new OrderDetail(pen, 10));
            order1.AddDetails(new OrderDetail(eraser, 2));
            order1.AddDetails(new OrderDetail(ruler, 1));

            Order order2 = new Order(2, john);
            order2.AddDetails(new OrderDetail(pencil, 3));
            order2.AddDetails(new OrderDetail(pen, 1));
            order2.AddDetails(new OrderDetail(ruler, 4));

            Order order3 = new Order(3, joe);
            order3.AddDetails(new OrderDetail(ruler, 8));
            order3.AddDetails(new OrderDetail(pencil, 5));
            order3.AddDetails(new OrderDetail(eraser, 2));

            goods.Add(pen);
            goods.Add(pencil);
            goods.Add(eraser);
            goods.Add(ruler);

            customers.Add(jack);
            customers.Add(john);
            customers.Add(joe);

            orderService.AddOrder(order1);
            orderService.AddOrder(order2);
            orderService.AddOrder(order3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void order_view_SelectionChanged(object sender, EventArgs e)
        {
            if (order_view.SelectedRows.Count > 0)
            {
                object dataBoundItem = order_view.SelectedRows[0].DataBoundItem;
                orderDetail_view.DataSource = orderService.QueryById(((Order)dataBoundItem).Id).GetOrderDetails();
            }

        }
        private void delete_button_Click(object sender, EventArgs e)
        {
            object dataBoundItem = order_view.SelectedRows[0].DataBoundItem;
            Order order = (Order)dataBoundItem;
            if (order == null)
            {
                MessageBox.Show("请选择一个订单进行删除");
                return;
            }
            DialogResult result = MessageBox.Show($"确认要删除Id为{order.Id}的订单吗？", "删除", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                orderService.RemoveOrder(order.Id);
                List<Order> resultList = orderService.QueryAll();
                order_view.DataSource = resultList;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void order_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void orderDetail_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void query_button_Click(object sender, EventArgs e)
        {
            string queryContent = textBox1.Text;
            string queryType = comboBox1.Text;
            switch (queryType)
            {
                //"ID", "客户名", "商品名", "总价格大于"
                case "ID":
                    Order order = orderService.QueryById(int.Parse(queryContent));
                    List<Order> result = new List<Order>();
                    if (order != null) result.Add(order);
                    order_view.DataSource = result;
                    break;
                case "客户名":
                    order_view.DataSource = orderService.QueryByCustomerName(queryContent);
                    break;
                case "商品名":
                    order_view.DataSource = orderService.QueryByGoodsName(queryContent);
                    break;
                case "总价格大于":
                    order_view.DataSource = orderService.QueryByTotalPrice(float.Parse(queryContent));
                    break;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        
    }
}
