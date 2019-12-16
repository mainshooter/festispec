//class DrawingDict
//{
//    constructor()
//    {
//        this.canvasMap = new Map;
//    }

//    addCanvas(canvas, ID)
//    {
//        this.canvasMap.set(ID, canvas);
        
//    }

//    getCanvas(ID)
//    {
//        return this.canvasMap.get(ID);
//    }
//}

class DrawQuestion {

    constructor(id) {
        this.scope = document.querySelector(id);
        this.canDraw = false;
        this.addListeners();
        this.drawings = [];
    }

    addListeners() {
        this.scope.addEventListener('mousedown', (event) => {
            this.canDraw = true;
        });
        this.scope.addEventListener('mouseup', (event) => {
            this.canDraw = false;
        });
        this.scope.addEventListener('mousemove', (event) => {
            let target = event.target;
            var rect = target.getBoundingClientRect();
            let x = event.clientX - rect.left; //x position within the element.
            let y = event.clientY - rect.top;  //y position within the element.
            this.handleEvent(x, y);
        });
        this.scope.addEventListener('touchmove', (event) => {
            var x = event.touches[0].clientX;
            var y = event.touches[0].clientY;
            this.handleEvent(x, y);
        });
    }

    handleEvent(x, y) {
        if (this.canDraw === false) {
            return;
        }
        let inkt = document.createElement('div');
        inkt.className = "inkt";
        inkt.style.top = y + "px";
        inkt.style.left = x + "px";

        this.scope.appendChild(inkt);
        this.drawings.push({
            "x": x,
            "y": y
        });
        this.scope.querySelector("input").value = JSON.stringify(this.drawings);
    }

}