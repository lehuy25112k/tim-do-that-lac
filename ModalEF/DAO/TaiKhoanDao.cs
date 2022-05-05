using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModalEF.EF;
using PagedList;

namespace ModalEF.DAO
{
    public class TaiKhoanDao
    {
        ContextDB db = null;
        public TaiKhoanDao()
        {
            db = new ContextDB();
        }

        //Đăng nhập
        public int Login(string userMail, string passWord)
        {
            var result = db.TaiKhoans.SingleOrDefault(x => x.Email == userMail);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.MatKhau == passWord)
                {

                    return 1;

                }
                else
                    return 0;
            }

        }

        public TaiKhoan GetById(string userMail)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.Email == userMail);
        }

        //Phân trang
        public IEnumerable<TaiKhoan> ListAllPaging(string searchString1, int page, int pageSize)
        {
            IQueryable<TaiKhoan> model = db.TaiKhoans;
            if (!string.IsNullOrEmpty(searchString1))
            {
                model = model.Where(x => x.Ten.Contains(searchString1));
            }

            return model.OrderByDescending(x => x.Ten).ToPagedList(page, pageSize);
        }

        //Xoá
        public bool Delete(string id)
        {
            try
            {
                var nv = db.TaiKhoans.Find(id);
                db.TaiKhoans.Remove(nv);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
