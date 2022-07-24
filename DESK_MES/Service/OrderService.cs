using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class OrderService
    {
        public List<OrderVO> GetOrderList()  // 주문 정보 가져오기
        {
            OrderDAC dac = new OrderDAC();
            List<OrderVO> result = dac.GetOrderList();
            dac.Dispose();

            return result;
        }
        public List<OrderDetailVO> GetOrderDetailList(int no)  // 주문 상세정보 가져오기
        {
            OrderDAC dac = new OrderDAC();
            List<OrderDetailVO> result = dac.GetOrderDetailList(no);
            dac.Dispose();

            return result;
        }
        public OrderVO GetOrderListByOrderNO(int no)  // 주문 상세정보 가져오기
        {
            OrderDAC dac = new OrderDAC();
            OrderVO result = dac.GetOrderListByOrderNO(no);
            dac.Dispose();

            return result;
        }

        public List<ProductVO> GetProductListForOrder()
        {
            OrderDAC dac = new OrderDAC();
            List<ProductVO> result = dac.GetProductListForOrder();
            dac.Dispose();

            return result;
        }

        public bool RegisterOrder(OrderVO order, List<OrderDetailVO> orderList)
        {
            OrderDAC dac = new OrderDAC();
            bool result = dac.RegisterOrder(order, orderList);
            dac.Dispose();

            return result;
        }

        public bool UpdateOrderInfo(OrderVO order)
        {
            OrderDAC dac = new OrderDAC();
            bool result = dac.UpdateOrderInfo(order);
            dac.Dispose();

            return result;
        }
    }
}
