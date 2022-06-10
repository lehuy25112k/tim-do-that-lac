using ModalEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModalEF.DAO
{
    public class DanhMucDao
    {
        ContextDB db = null;

        public DanhMucDao()
        {
            db = new ContextDB();
        }

        //Phân trang
        public IEnumerable<DanhMuc> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<DanhMuc> model = db.DanhMucs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenDanhMuc.Contains(searchString));
            }

            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
        }

        //Xoá
        public bool Delete(int id)
        {
            try
            {
                var dm = db.DanhMucs.Find(id);
                db.DanhMucs.Remove(dm);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            { return false; }
        }

        //Xử lý
        public List<DanhMuc> ListFeatureDanhMuc(int top)
        {
            return db.DanhMucs.OrderByDescending(x => x.LuotTim).Take(top).ToList();
        }
    }
}
