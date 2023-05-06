
// вспомогательная функция для доступа к элементам DOM
$ = id => document.getElementById(id);

window.addEventListener("load", () => {

    let btnShowPass = $("showPassword");
    let inputPassword = $("Password");


    console.log(`Btn: ${btnShowPass}`);

    //Найти кнопку включения/отключения видимости
    btnShowPass.addEventListener("click", (e) => {
        console.log(`Input type: ${inputPassword.type}`);

        if (inputPassword.type === "password") {
            inputPassword.type = "text";
            btnShowPass.innerHTML = `<i class="bi bi-eye-slash"></i>`
        }
        else if (inputPassword.type === "text") {
            inputPassword.type = "password";
            btnShowPass.innerHTML = `<i class="bi bi-eye"></i>`
        }

    });
    
})