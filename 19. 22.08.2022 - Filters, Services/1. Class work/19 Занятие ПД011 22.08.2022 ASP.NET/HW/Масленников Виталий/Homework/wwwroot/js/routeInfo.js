$ = id => document.getElementById(id);
$("routesTable").addEventListener("click",
    (e) => {

        if(!e.target.dataset.id)
            return;
        
        let xhr = new XMLHttpRequest();
        xhr.open("GET", `/Routes/RouteInfo/${e.target.dataset.id}`);
        xhr.send();

        xhr.onload = () => {
            let route = JSON.parse(xhr.response);
            $("infoId").innerHTML =  `${route.id}`;
            $("infoStartPoint").innerHTML =   `${route.startPoint}`;
            $("infoMiddlePoint").innerHTML =  `${route.middlePoint}`;
            $("infoFinishPoint").innerHTML =  `${route.finishPoint}`;
            $("infoLength").innerHTML =       `${route.length}`;
            $("infoDifficulty").innerHTML =   `${route.difficulty}`;
            $("infoInstructor").innerHTML =   `${route.instructor}`;
        };

        //bootstrap.Modal.getOrCreateInstance($('routeInfoModal')).show();
    });