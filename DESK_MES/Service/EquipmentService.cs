using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESK_DTO;
using System.Data.SqlClient;

namespace DESK_MES
{
    public class EquipmentService
    {
        public List<EquipmentVO> SelectEquipmentAllList()
        {
            EquipmentDAC db = new EquipmentDAC();
            List<EquipmentVO> list = db.SelectEquipmentAllList();
            db.Dispose();
            return list;
        }
        public List<EquipmentVO> SelectOperationTypeList()
        {
            EquipmentDAC db = new EquipmentDAC();
            List<EquipmentVO> list = db.SelectOperationTypeList();
            db.Dispose();
            return list;
        }
        public bool InsertEquipmentList(EquipmentVO equi)
        {
            EquipmentDAC db = new EquipmentDAC();
            bool result = db.InsertEquipment(equi);
            db.Dispose();
            return result;
        }
        public bool UpdateEquipment(EquipmentVO equi)
        {
            EquipmentDAC db = new EquipmentDAC();
            bool result = db.UpdateEquipment(equi);
            db.Dispose();
            return result;
        }
        public bool DeleteEquipment(int equiNo) //삭제
        {
            EquipmentDAC db = new EquipmentDAC();
            bool result = db.DeleteEquipment(equiNo);
            db.Dispose();
            return result;
        }
    }
}