using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly QuizDbContext _context;

        public ParticipantController(QuizDbContext context)
        {
            _context = context;
        }

        // GET: api/Participant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant>>> Getparticipants()
        {
          if (_context.participants == null)
          {
              return NotFound();
          }
            return await _context.participants.ToListAsync();
        }

        // GET: api/Participant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
          if (_context.participants == null)
          {
              return NotFound();
          }
            var participant = await _context.participants.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            return participant;
        }

        // PUT: api/Participant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant(int id, ParticipantResult _participant)
        {
            if (id != _participant.ParticipantId)
            {
                return BadRequest();
            }

            // get all current details of the record, update with quiz result
            Participant participant = _context.participants.Find(id);
            participant.Score = _participant.Score;
            participant.TimeTaken = _participant.TimeTaken;


            _context.Entry(participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Participant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipant(Participant participant)
        {
          if (_context.participants == null)
          {
              return Problem("Entity set 'QuizDbContext.participants'  is null.");
          }

            var temp = _context.participants
                  .Where(x => x.Name == participant.Name
                  && x.Email == participant.Email).FirstOrDefault();

            if (temp == null)
            {
                _context.participants.Add(participant);
                await _context.SaveChangesAsync();
            }
            else
            {
                participant = temp;
            }

            return Ok(participant);
            //return CreatedAtAction("GetParticipant", new { id = participant.ParticipantId }, participant);
        }

        // DELETE: api/Participant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            if (_context.participants == null)
            {
                return NotFound();
            }
            var participant = await _context.participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            _context.participants.Remove(participant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipantExists(int id)
        {
            return (_context.participants?.Any(e => e.ParticipantId == id)).GetValueOrDefault();
        }
    }
}
