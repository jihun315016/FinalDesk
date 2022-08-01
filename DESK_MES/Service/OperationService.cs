using DESK_DTO;
using DESK_MES.DAC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESK_MES.Service
{
    public class OperationService
    {
        public List<OperationVO> GetOperationList(int no = 0)
        {
            OperationDAC dac = new OperationDAC();
            List<OperationVO> list = dac.GetOperationList(no);
            dac.Dispose();
            return list;
        }

        public DataSet GetOIRelation()
        {
            OperationDAC dac = new OperationDAC();
            DataSet ds = dac.GetOIRelation();
            dac.Dispose();
            return ds;
        }

        public bool SaveOperation(OperationVO oper)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.SaveOperation(oper);
            dac.Dispose();
            return result;
        }

        public List<int> GetInspectListByOperation(int operNo)
        {
            OperationDAC dac = new OperationDAC();
            DataTable dt = dac.GetInspectListByOperation(operNo);
            List<int> list = new List<int>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(Convert.ToInt32(dr["Inspect_No"]));
            }
            dac.Dispose();
            return list;
        }

        public List<string> GetProductListByOperation(int operNo)
        {
            OperationDAC dac = new OperationDAC();
            DataTable dt = dac.GetInspectListByOperation(operNo);
            List<string> list = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr["Product_Code"].ToString());
            }
            dac.Dispose();
            return list;
        }

        public List<int> GetEquipmentListByOperation(int operNo)
        {
            OperationDAC dac = new OperationDAC();
            DataTable dt = dac.GetEquipmentListByOperation(operNo);
            List<int> list = new List<int>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(Convert.ToInt32(dr["Equipment_No"]));
            }
            dac.Dispose();
            return list;
        }

        public bool SaveOIRelation(int operNo, List<InspectItemVO> inspectList)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.SaveOIRelation(operNo, inspectList);
            dac.Dispose();
            return result;
        }

        public bool SaveOPRelation(int operNo, List<ProductVO> productList)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.SaveOPRelation(operNo, productList);
            dac.Dispose();
            return result;
        }

        public bool SaveOERelation(int operNo, List<EquipmentVO> equipmentList)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.SaveOERelation(operNo, equipmentList);
            dac.Dispose();
            return result;
        }

        public bool DeleteOIIetm(int operNo)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.DeleteOIIetm(operNo);
            dac.Dispose();
            return result;
        }

        public bool DeleteEOItem(int operNo)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.DeleteEOItem(operNo);
            dac.Dispose();
            return result;
        }

        public bool UpdateOperation(OperationVO oper)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.UpdateOperation(oper);
            dac.Dispose();
            return result;
        }

        public bool DeleteOperation(int operNo)
        {
            OperationDAC dac = new OperationDAC();
            bool result = dac.DeleteOperation(operNo);
            dac.Dispose();
            return result;
        }
    }
}
