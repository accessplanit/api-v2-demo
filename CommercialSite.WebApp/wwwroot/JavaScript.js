window.initializeSidebarCollapse = () => {
    const sidebarCollapseButton = document.querySelector('#sidebar-collapse-button');

    if (sidebarCollapseButton) {
        sidebarCollapseButton.addEventListener('click', () => {
            document.querySelector('#sidebar').classList.toggle("expand");
        });
    }
};

// Idle timeout. Last activity is kept in localStorage so every open tab shares one clock,
// and so a reload after a long idle period can be refused a restored session.
window.idleTimer = (() => {
    const storageKey = "lastActivity";
    const activityEvents = ["mousemove", "mousedown", "keydown", "scroll", "touchstart", "click"];
    let dotNetRef = null;
    let timeoutMs = 0;
    let warningMs = 0;
    let intervalId = null;
    let warningShown = false;
    let lastWrite = 0;

    const lastActivity = () => {
        const stored = parseInt(localStorage.getItem(storageKey), 10);
        return isNaN(stored) ? Date.now() : stored;
    };

    const touch = () => {
        if (warningShown) {
            return; // once warned, only "Stay signed in" extends the session
        }
        const now = Date.now();
        if (now - lastWrite > 5000) {
            lastWrite = now;
            localStorage.setItem(storageKey, String(now));
        }
    };

    const check = () => {
        const idleFor = Date.now() - lastActivity();
        const remaining = timeoutMs - idleFor;

        if (remaining <= 0) {
            stop();
            dotNetRef.invokeMethodAsync("OnIdleTimeout");
        } else if (remaining <= warningMs) {
            warningShown = true;
            dotNetRef.invokeMethodAsync("OnIdleWarning", Math.ceil(remaining / 1000));
        } else if (warningShown) {
            // Activity in another tab extended the session
            warningShown = false;
            dotNetRef.invokeMethodAsync("OnIdleReset");
        }
    };

    const stop = () => {
        activityEvents.forEach(e => document.removeEventListener(e, touch, true));
        if (intervalId !== null) {
            clearInterval(intervalId);
            intervalId = null;
        }
        warningShown = false;
    };

    return {
        start: (ref, timeoutMilliseconds, warningMilliseconds) => {
            stop();
            dotNetRef = ref;
            timeoutMs = timeoutMilliseconds;
            warningMs = warningMilliseconds;
            lastWrite = 0;
            localStorage.setItem(storageKey, String(Date.now()));
            activityEvents.forEach(e => document.addEventListener(e, touch, true));
            intervalId = setInterval(check, 1000);
        },
        reset: () => {
            warningShown = false;
            lastWrite = Date.now();
            localStorage.setItem(storageKey, String(Date.now()));
        },
        stop: () => {
            stop();
            localStorage.removeItem(storageKey);
        }
    };
})();
