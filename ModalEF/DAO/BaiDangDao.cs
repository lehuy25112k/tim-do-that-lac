using ModalEF.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModalEF.DAO
{
    public class BaiDangDao
    {
        ContextDB db = null;

        public BaiDangDao()
        {
            db = new ContextDB();
        }

        //Phân trang
        public IEnumerable<BaiDang> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<BaiDang> model = db.BaiDangs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TieuDe.Contains(searchString) || x.NoiDung.Contains(searchString));
            }

            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
        }

        public IEnumerable<BaiDang> ListAllPagings(string searchString, int page, int pageSize)
        {
            IQueryable<BaiDang> model = db.BaiDangs;
            model = model.Where(x => x.TrangThaiDuyet == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TieuDe.Contains(searchString) || x.NoiDung.Contains(searchString));
            }

            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
        }

        //Đổi trạng thái
        public bool ChangeStatus(int id)
        {
            var baiDang = db.BaiDangs.Find(id);
            baiDang.TrangThaiDuyet = !baiDang.TrangThaiDuyet;
            db.SaveChanges();
            return baiDang.TrangThaiDuyet;
        }

        //Xoá
        public bool Delete(int id)
        {
            try
            {
                var bd = db.BaiDangs.Find(id);
                db.BaiDangs.Remove(bd);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            { return false; }
        }

        //Xử lý logic
        public List<BaiDang> ListFeatureBaiDang(int top)
        {
            return db.BaiDangs.Where(x => x.LuotXem > 5).OrderByDescending(x => x.NgayTao).Take(top).ToList();
        }
    }
}
