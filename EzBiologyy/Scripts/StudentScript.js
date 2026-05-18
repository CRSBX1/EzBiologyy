/* ============================================================
   EzBiology — main.js
   ============================================================ */

/* ── ACCORDION ── */
function toggleAcc(id) {
  const body = document.getElementById(id + '-body');
  const chev = document.getElementById(id + '-chev');
  if (!body || !chev) return;
  body.classList.toggle('open');
  chev.classList.toggle('open');
}

/* ── MARK AS DONE ── */
function toggleDone(btn) {
  btn.classList.toggle('done');
  btn.textContent = btn.classList.contains('done') ? '✓ Done' : 'Mark as done';
}

/* ── COURSE TABS ── */
function switchTab(tab, btn) {
  document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
  btn.classList.add('active');
  document.getElementById('tab-enrolled').style.display  = tab === 'enrolled'  ? '' : 'none';
  document.getElementById('tab-available').style.display = tab === 'available' ? '' : 'none';
}

/* ── FORUM FILTER ── */
function setFilter(filter, btn) {
  document.querySelectorAll('.filter-btn').forEach(b => b.classList.remove('active'));
  btn.classList.add('active');
  document.querySelectorAll('.forum-post-card').forEach(card => {
    if (filter === 'all') { card.style.display = ''; return; }
    const tag = card.querySelector('.forum-tag');
    const match = (filter === 'questions' && tag && tag.classList.contains('tag-question')) ||
                  (filter === 'discussions' && tag && tag.classList.contains('tag-discussion'));
    card.style.display = match ? '' : 'none';
  });
}

/* ── REPLY TEXTAREA AUTO-RESIZE ── */
document.addEventListener('DOMContentLoaded', () => {
  document.querySelectorAll('.reply-input').forEach(ta => {
    ta.addEventListener('input', function () {
      this.style.height = 'auto';
      this.style.height = this.scrollHeight + 'px';
    });
  });
});
