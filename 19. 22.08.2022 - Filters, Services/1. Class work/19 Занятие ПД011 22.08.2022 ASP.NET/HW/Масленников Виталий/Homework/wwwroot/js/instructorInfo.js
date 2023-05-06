$ = id => document.getElementById(id);
$("insTable").addEventListener("click",
    (e) => {

        if(!e.target.dataset.id)
            return;
        
        let xhr = new XMLHttpRequest();
        xhr.open("GET", `/Instructors/InstructorInfo/${e.target.dataset.id}`);
        xhr.send();

        xhr.onload = () => {
            let instructor = JSON.parse(xhr.response);
            let dob = new Date(instructor.doB).toLocaleDateString();
            $("id").innerHTML =  `${instructor.id}`;
            $("surname").innerHTML =  `${instructor.surname}`;
            $("name").innerHTML =  `${instructor.name}`;
            $("patronymic").innerHTML =  `${instructor.patronymic}`;
            $("dob").innerHTML =  `${dob}`;
            $("category").innerHTML =  `${instructor.category}`;
            $("shortName").innerHTML =  `${instructor.shortName}`;
        };
    });