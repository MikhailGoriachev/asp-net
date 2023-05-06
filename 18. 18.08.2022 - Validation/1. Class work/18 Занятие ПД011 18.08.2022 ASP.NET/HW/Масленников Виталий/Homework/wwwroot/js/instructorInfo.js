$ = id => document.getElementById(id);
$("insTable").addEventListener("click",
    (e) => {
        let target;
        if(e.target.classList.contains("instructor-info"))
            target = e.target;
        else if(e.target.parentElement.classList.contains("instructor-info"))
            target = e.target.parentElement;
        else return;

        let xhr = new XMLHttpRequest();
        xhr.open("GET", `/Instructors/InstructorInfo/${target.dataset.id}`);
        xhr.send();

        xhr.onload = () => {
            let instructor = JSON.parse(xhr.response);
            let dob = new Date(instructor.doB).toLocaleDateString();
            $("idSpan").innerHTML =  `${instructor.id}`;
            $("surnameSpan").innerHTML =  `${instructor.surname}`;
            $("nameSpan").innerHTML =  `${instructor.name}`;
            $("patronymicSpan").innerHTML =  `${instructor.patronymic}`;
            $("dobSpan").innerHTML =  `${dob}`;
            $("categorySpan").innerHTML =  `${instructor.category}`;
        };

        bootstrap.Modal.getOrCreateInstance($('instrInfoModal')).show();
    });