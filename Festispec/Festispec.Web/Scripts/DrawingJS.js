class DrawQuestion {

    constructor(id) {
        this.scope = document.querySelector(id);
        this.canDraw = false;
        this.addListeners();
		this.drawings = [];
		this.scope.querySelector("img").setAttribute('draggable', false);
    }

    addListeners() {
		this.scope.addEventListener('mousedown', (event) => {
			this.canDraw = true;
        });

		document.addEventListener('mouseup', (event) => {
			this.canDraw = false;
        });

        this.scope.addEventListener('mousemove', (event) => {
            let target = event.target;
            var rect = target.getBoundingClientRect();
            let x = event.clientX - rect.left; //x position within the element.
            let y = event.clientY - rect.top;  //y position within the element.
			this.handleEvent(x, y);
        });

		this.scope.addEventListener('touchstart', (event) => {
			document.querySelector("body").style.height = "100%";
			document.querySelector("body").style.overflow = "hidden";
        });

		this.scope.addEventListener('touchend', (event) => {
			document.querySelector("body").style.height = "100%";
			document.querySelector("body").style.overflow = "";
        });

		this.scope.addEventListener('touchmove', (event) => {
			this.canDraw = true;
			let backgroundImage = this.scope.querySelector("img");
			var realTarget = document.elementFromPoint(event.touches[0].clientX, event.touches[0].clientY);
			if (realTarget == backgroundImage) {
				let target = event.target;
				var rect = target.getBoundingClientRect();
				let x = event.touches[0].clientX - rect.left; //x position within the element.
                let y = event.touches[0].clientY - rect.top;  //y position within the element.
                this.handleEvent(x, y);
			}
			this.canDraw = false;
        });
    }

    convertXAndYToCorrectPosition(x, y) {
        let userControlWidth = 620;
        let userControlHeight = 300;

        let image = this.scope.querySelector("img");
        let imageWidth = image.offsetWidth;
        let imageHeight = image.offsetHeight;

        let widthRatio = imageWidth / userControlWidth;
        let heightRatio = imageHeight / userControlHeight;

        x = x * widthRatio;
        y = y * heightRatio;
        return ({
            x: x,
			y: y,
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

        let convertResult = this.convertXAndYToCorrectPosition(x, y);

        this.scope.appendChild(inkt);
        this.drawings.push({
            "x": convertResult["x"],
            "y": convertResult["y"]
		});
		this.scope.querySelector("input").value = JSON.stringify(this.drawings);
	}
}