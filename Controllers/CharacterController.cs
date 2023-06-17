using EF_Relationships.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;



/*
 User - Character   -->  1:N
 Character - Weapon  -->  1:1
 Character - Skill  -->  N:M
 */
namespace EF_Relationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly RelationDbContext _context;
        public CharacterController(RelationDbContext context)
        {
                _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get(int userId)
        {
            var characters = await _context.Characters
                .Where(c => c.UserId == userId)
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .ToListAsync();
            return new OkObjectResult(characters);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> Create(CharacterDto character)
        {
            var user = await _context.USers.FindAsync(character.UserId);
            if (user == null)
                return NotFound();

            var newCharacter = new Character 
            { 
                UserId = character.UserId,
                Name = character.Name,
                RpgClass = character.RpgClass,
                User = user
            };
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return await Get(newCharacter.UserId);
        }
        
        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> AddWeapon(WeaponDto weapon)
        {
            var character = await _context.Characters.FindAsync(weapon.CharacterId);
            if (character == null)
                return NotFound();

            var newWeapon = new Weapon
            {
               CharacterId = weapon.CharacterId,
               Damage = weapon.Damage,
               Name = weapon.Name
            };
            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();

            return character;
        }

        [HttpPost("skill")]
        public async Task<ActionResult<Character>> AddSkill(CharacterSkillDto chSkill)
        {
            var character = await _context.Characters
                .Where(c => c.Id == chSkill.CharacterId)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync();
            if (character == null)
                return NotFound();

            var skill = await _context.Skills.FindAsync(chSkill.SkillId);
            if (skill == null)
                return NotFound();


            character.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return character;
        }
    }
}
