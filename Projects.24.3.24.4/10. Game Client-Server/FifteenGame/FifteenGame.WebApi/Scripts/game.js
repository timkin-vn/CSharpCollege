const T = [
    ['#fda4af', '#93c5fd', '#86efac', '#fde047', '#c4b5fd', '#f472b6', '#fdba74', '#67e8f9'],
    ['🐱', '🐶', '🐼', '🦊', '🐰', '🦁', '🐵', '🐸'],
    ['🍎', '🍌', '🍓', '🍇', '🍉', '🍍', '🍒', '🥝']
];

let currentDiff = 1;
let currentTheme = 0;

let cards = [], f = null, s = null, lock = false;

let moves = 0, match = 0, total = 0, sec = 0, timer = null;
let hints = 2, combo = 1, score = 0;

function isColorTheme()
{
    return currentTheme === 0;
}

function init() {

    clearInterval(timer);

    moves = match = sec = score = 0;
    hints = 2;
    combo = 1;

    f = s = null;
    lock = false;

    document.getElementById('time').textContent = '00:00';
    document.getElementById('moves').textContent = 0;
    document.getElementById('score').textContent = 0;
    document.getElementById('combo').textContent = 'x1';
    document.getElementById('hLeft').textContent = 2;
    document.getElementById('btnHint').disabled = false;

    const r = currentDiff === 1 ? 2 : (currentDiff === 2 ? 3 : 4);
    const c = 4;

    total = (r * c) / 2;

    const b = document.getElementById('board');
    b.style.gridTemplateColumns = 'repeat(' + c + ', 65px)';
    b.innerHTML = '';

    cards = [];
    for (let i = 0; i < total; i++) {
        cards.push({ i: i, v: T[currentTheme][i] }, { i: i, v: T[currentTheme][i] });
    }

    cards.sort(() => Math.random() - 0.5).forEach((item) => {
        const el = document.createElement('div');
        el.className = 'card';
        el.textContent = '';
        el.onclick = () => flip(el, item);
        b.appendChild(el);
    });

    timer = setInterval(() => {
        sec++;
        const m = String(Math.floor(sec / 60)).padStart(2, '0');
        const sc = String(sec % 60).padStart(2, '0');
        document.getElementById('time').textContent = m + ':' + sc;
    }, 1000);
}

function applyCardAppearance(el, item, isOpened) {
    if (isOpened) {
        if (isColorTheme()) {
            el.style.backgroundColor = item.v;
            el.style.borderColor = item.v;
            el.textContent = '';
        } else {
            el.style.backgroundColor = '#ffffff';
            el.style.borderColor = '#94a3b8';
            el.textContent = item.v;
        }
    } else {
        el.style.backgroundColor = '';
        el.style.borderColor = '';
        el.textContent = '';
    }
}

function flip(el, item) {
    if (lock || el.classList.contains('open') || el.classList.contains('matched')) return;
    el.classList.add('open');
    applyCardAppearance(el, item, true);

    if (!f) {
        f = { el: el, item: item };
        return;
    }

    s = { el: el, item: item };

    lock = true;

    document.getElementById('moves').textContent = ++moves;

    if (f.item.i === s.item.i) {
        f.el.classList.add('matched');
        s.el.classList.add('matched');
        score += 100 * combo++;
        document.getElementById('score').textContent = score;
        document.getElementById('combo').textContent = 'x' + combo;

        f = s = null;
        lock = false;

        if (++match === total) {
            clearInterval(timer);
            const t = document.getElementById('time').textContent;
            const prev = JSON.parse(localStorage.getItem('r_' + currentDiff)) || { s: 9999 };
            if (sec < prev.s) {
                localStorage.setItem('r_' + currentDiff, JSON.stringify({ t: t, s: sec, m: moves }));
            }
            loadR();
            setTimeout(() => alert('🎉 Победа!\nВремя: ' + t + '\nХоды: ' + moves + '\nОчки: ' + score), 200);
        }

    } else {
        combo = 1;
        document.getElementById('combo').textContent = 'x1';
        setTimeout(() => {
            f.el.className = s.el.className = 'card';
            applyCardAppearance(f.el, f.item, false);
            applyCardAppearance(s.el, s.item, false);
            f = s = null;
            lock = false;
        }, 700);
    }
}

document.getElementById('btnHint').onclick = () => {
    if (hints <= 0 || lock) return;
    const un = Array.from(document.querySelectorAll('.card:not(.matched)'));
    if (un.length < 2) return;
    document.getElementById('hLeft').textContent = --hints;
    score = Math.max(0, score - 50);
    document.getElementById('score').textContent = score;
    if (!hints) document.getElementById('btnHint').disabled = true;

    const firstIdx = Array.from(un[0].parentNode.children).indexOf(un[0]);
    const target = cards[firstIdx].i;
    const pair = un.filter(el => cards[Array.from(el.parentNode.children).indexOf(el)].i === target);

    pair.forEach(el => {
        el.classList.add('hint');
        const item = cards[Array.from(el.parentNode.children).indexOf(el)];
        applyCardAppearance(el, item, true);
    });

    lock = true;

    setTimeout(() => {
        pair.forEach(el => {
            el.classList.remove('hint');
            if (!el.classList.contains('open')) {
                const item = cards[Array.from(el.parentNode.children).indexOf(el)];
                applyCardAppearance(el, item, false);
            }
        });

        lock = false;

    }, 900);
};

function loadR() {
    for (let i = 1; i <= 3; i++) {
        const r = JSON.parse(localStorage.getItem('r_' + i));
        document.getElementById('r' + i).textContent = r ? (r.t + ' (' + r.m + 'х)') : '-';
    }
}

function setupTabs(groupId, onSelect) {
    const group = document.getElementById(groupId);
    if (!group) return;
    group.querySelectorAll('.tab-btn').forEach(btn => {
        btn.onclick = () => {
            group.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
            btn.classList.add('active');
            onSelect(parseInt(btn.dataset.val));
            init();
        };
    });
}

setupTabs('diffGroup', val => currentDiff = val);
setupTabs('themeGroup', val => currentTheme = val);
document.getElementById('btnNew').onclick = init;

loadR();

init();