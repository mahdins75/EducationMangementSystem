using DataBase.Repository;
using Mstech.Entity.Etity;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Implementation.ConstantService
{
    public class ConstantService : BaseService<Constant>
    {
        public ConstantService(IUnitOfWork _unitOfWork, IRepository<Constant> _repository) : base(_unitOfWork)
        {

        }

        public async Task<List<SelectListItem>> GetSelectListByEntityName(string EntityName)
        {
            var result = await this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted)
                .Where(m => m.EntityName == EntityName)
                .Select(m => new SelectListItem()
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }).ToListAsync();

            return result;
        }


        public async Task<List<Constant>> GetAllAsync()
        {
            return await this.GetAllAsIqueriable().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<int> CreateAsync(string name, int groupId)
        {
            if (await this.GetAllAsIqueriable().AnyAsync(x => x.Name == name.Trim() && x.EntityName == ""))
            {
                return -1;
            }

            try
            {
                var constant = new Constant()
                {
                    Name = name,
                    //GroupId = groupId,
                    CreateDate = DateTime.Now,
                    //Importance = 1,
                };

                var result = this.Insert(constant);
                return result.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<ConstantDataTableViewModel> LoadDataTable(ConstantDataTableViewModel dataTable)
        {
            string orderBy = string.Empty;
            string orderDir = string.Empty;
            if (dataTable.Order.Any())
            {
                orderBy = dataTable.Columns[dataTable.Order[0].Column].Name;
                orderDir = dataTable.Order[0].Dir.ToLower();
            }

            var query = this.GetAllAsIqueriable()
                .Where(x => x.EntityName == dataTable.Filter.EntityName)
                .Where(x => !x.IsDeleted);

            dataTable.RecordsTotal = await query.LongCountAsync();
            query = SetFilter(query, dataTable.Filter);
            query = SetOrder(query, orderBy, orderDir == "asc");
            dataTable.RecordsFiltered = await query.LongCountAsync();

            dataTable.Data = query
                .Skip(dataTable.Start)
                .Take(dataTable.Length)
                .ToList()
                .Select(x => new ConstantViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    //Importance = x.Importance,
                }).ToList();

            return dataTable;
        }

        public async Task<Constant> GetById(int id)
        {
            var constant = await this.GetAllAsIqueriable()
                .FirstOrDefaultAsync(m => m.Id == id);

            return constant;
        }

        public async Task<int> CreateAsync(ManageConstantViewModel model)
        {
            if (await this.GetAllAsIqueriable().AnyAsync(x => x.Name == model.Name.Trim()))
            {
                return -1;
            }

            try
            {
                var entity = model.Adapt<Constant>();
                entity.Code = "1";
                entity.CreateDate = DateTime.Now;
                var result = this.Insert(entity);

                return result.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> EditAsync(ManageConstantViewModel model)
        {
            var constant = await this.GetAllAsIqueriable()
                .FirstOrDefaultAsync(m => m.Id == model.Id);

            if (await this.GetAllAsIqueriable().AnyAsync(x => x.Name == model.Name.Trim() && x.Id != model.Id))
            {
                return -1;
            }

            constant.Name = model.Name;
            constant.ModifyDate = DateTime.Now;

            try
            {
                var result = this.Update(constant);
                return result.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var model = await this.GetAllAsIqueriable().SingleOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return false;
            }
            model.IsDeleted = true;

            var result = this.Update(model);
            return true;
        }

        #region Private
        private IQueryable<Constant> SetFilter(IQueryable<Constant> query, ConstantFilterViewModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(r => r.Name.Contains(filter.Name));
            }

            return query;
        }

        private IQueryable<Constant> SetOrder(IQueryable<Constant> query, string sortField, bool sortDir)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                query = !sortDir ? query.OrderBy(r => r.Id) : query.OrderByDescending(r => r.Id);
            }
            else
            {
                switch (sortField)
                {
                    case "Id":
                        query = sortDir ? query.OrderBy(r => r.Id) : query.OrderByDescending(r => r.Id);
                        break;
                    case "Name":
                        query = sortDir ? query.OrderBy(r => r.Name) : query.OrderByDescending(r => r.Name);
                        break;
                }
            }

            return query;
        }

        #endregion
    }

}