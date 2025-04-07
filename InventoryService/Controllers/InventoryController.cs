//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using InventoryService.Interface;
//using InventoryService.Repositories;
//using Microsoft.AspNetCore.Authorization;
//using InventoryService.Models;

//namespace InventoryService.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class InventoryController : ControllerBase
//    {
//        private readonly IInventory _inventoryRepository;
//        private readonly IDepartment _departmentRepository;
//        public InventoryController(IInventory inventoryRepository, IDepartment departmentRepository)
//        {
//            _inventoryRepository = inventoryRepository;
//            _departmentRepository = departmentRepository;

//        }


//        [HttpGet]
//        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can view
//        public async Task<IActionResult> GetAllInventoryReq()
//        {
//            var inventoryItems = await _inventoryRepository.GetAllInventoryRequest();
//            if (inventoryItems == null)
//            {
//                return NotFound("No Inventory Request Created");
//            }
//            return Ok(inventoryItems);
//        }

//        [HttpGet("inventory/{id}")]
//        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can view
//        public async Task<IActionResult> GetInventoryReqofDepartment(string departmentName)
//        {
//            var item = await _inventoryRepository.GetInventoryReqofDepartment(departmentName);
//            if (item == null)
//            {
//                return NotFound("Item not found.");
//            }
//            return Ok(item);
//        }

//        [HttpGet("inventory/{itemName}")]
//        [Authorize(Roles = "Admin,Manager")]
//        public async Task<IActionResult> GetItembyName(string itemName)
//        {
//            var inventoryItem = await _inventoryRepository.GetInventorybyItem(itemName);
//            if (inventoryItem == null)
//            {
//                return NotFound("Inventory not found");
//            }
//            return Ok(inventoryItem);
//        }

//        [HttpPost("create")]
//        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can create
//        public async Task<IActionResult> AddInventoryItem(Inventory inventory)
//        {
//            var newItem = await _inventoryRepository.CreateInventoryRequest(inventory);
//            return CreatedAtAction(nameof(GetItembyName), new { id = newItem.InventoryId }, newItem);
//        }

//        [HttpPut("update/{id}")]
//        [Authorize(Roles = "Admin,Manager")] // Only Admin and Manager can update
//        public async Task<IActionResult> UpdateInventoryItem(int id, Inventory updatedInventory)
//        {
//            if (id != updatedInventory.InventoryId)
//            {
//                return BadRequest("Inventory ID mismatch.");
//            }

//            var item = await _inventoryRepository.GetInventorybyItemID(id);
//            if (item == null)
//            {
//                return NotFound("Item not found.");
//            }

//            var inventory = await _inventoryRepository.UpdateInventoryRequest(id, updatedInventory);
//            return Ok(inventory);
//        }

//        [HttpDelete("{id}")]
//        [Authorize(Roles = "Admin")] // Only Admin can delete
//        public async Task<IActionResult> DeleteInventoryItem(int id)
//        {
//            var item = await _inventoryRepository.GetInventorybyItemID(id);
//            if (item == null)
//            {
//                return NotFound("Item not found.");
//            }

//            await _inventoryRepository.RemoveInventoryRequest(id);
//            return NoContent();
//        }

//    }
//}


using InventoryService.DTO;
using InventoryService.Interface;
using InventoryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventory _inventoryRepo;
        private readonly IDepartment _departmentRepo;

        public InventoryController(IInventory inventoryRepo, IDepartment departmentRepo)
        {
            _inventoryRepo = inventoryRepo;
            _departmentRepo = departmentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _inventoryRepo.GetAllInventoryRequest();
            var result = items.Select(i => new InventoryDTO
            {
                InventoryId = i.InventoryId,
                ItemName = i.ItemName,
                Quantity = i.Quantity,
                Price = i.Price,
                DepartmentName = i.Department.DepartmentName
            });

            return Ok(result);
        }

        [HttpGet("by-department/{departmentName}")]
        public async Task<IActionResult> GetByDepartment(string departmentName)
        {
            var items = await _inventoryRepo.GetInventoryReqofDepartment(departmentName);
            var result = items.Select(i => new InventoryDTO
            {
                InventoryId = i.InventoryId,
                ItemName = i.ItemName,
                Quantity = i.Quantity,
                Price = i.Price,
                DepartmentName = i.Department.DepartmentName
            });

            return Ok(result);
        }

        [HttpGet("by-item/{itemName}")]
        public async Task<IActionResult> GetByItem(string itemName)
        {
            var items = await _inventoryRepo.GetInventorybyItem(itemName);
            var result = items.Select(i => new InventoryDTO
            {
                InventoryId = i.InventoryId,
                ItemName = i.ItemName,
                Quantity = i.Quantity,
                Price = i.Price,
                DepartmentName = i.Department.DepartmentName
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _inventoryRepo.GetInventorybyItemID(id);
            if (item == null) return NotFound();

            var result = new InventoryDTO
            {
                InventoryId = item.InventoryId,
                ItemName = item.ItemName,
                Quantity = item.Quantity,
                Price = item.Price,
                DepartmentName = item.Department.DepartmentName
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInventoryDTO dto)
        {
            int deptId = await _departmentRepo.GetDepartmentID(dto.DepartmentName);
            if (deptId == 0) return BadRequest("Invalid department");

            var inventory = new Inventory
            {
                ItemName = dto.ItemName,
                Quantity = dto.Quantity,
                Price = dto.Price,
                DepartmentId = deptId
            };

            var created = await _inventoryRepo.CreateInventoryRequest(inventory);
            return CreatedAtAction(nameof(GetById), new { id = created.InventoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateInventoryDTO dto)
        {
            int deptId = await _departmentRepo.GetDepartmentID(dto.DepartmentName);
            if (deptId == 0) return BadRequest("Invalid department");

            var updatedInventory = new Inventory
            {
                ItemName = dto.ItemName,
                Quantity = dto.Quantity,
                Price = dto.Price,
                DepartmentId = deptId
            };

            var result = await _inventoryRepo.UpdateInventoryRequest(id, updatedInventory);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _inventoryRepo.RemoveInventoryRequest(id);
            return NoContent();
        }
    }
}
