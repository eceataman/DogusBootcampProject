document.addEventListener("DOMContentLoaded", function () {
    const toggleBtn = document.getElementById("themeToggleBtn");

    const currentTheme = localStorage.getItem("theme") || "light";
    document.documentElement.setAttribute("data-theme", currentTheme); // ⚠️ Burası şart!

    if (toggleBtn) {
        toggleBtn.innerText = currentTheme === "dark" ? "☀️" : "🌙";

        toggleBtn.addEventListener("click", function () {
            const newTheme = document.documentElement.getAttribute("data-theme") === "dark" ? "light" : "dark";
            document.documentElement.setAttribute("data-theme", newTheme);
            localStorage.setItem("theme", newTheme);
            toggleBtn.innerText = newTheme === "dark" ? "☀️" : "🌙";
        });
    }
});
