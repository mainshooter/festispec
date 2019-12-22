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
			window.addEventListener('scroll', noScroll);
			this.canDraw = true;
        });
		document.addEventListener('mouseup', (event) => {
			window.removeEventListener('scroll', noScroll);
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

		console.log(document.body.style.overflow);
		this.scope.querySelector("input").value = JSON.stringify(this.drawings);

	}




}
function noScroll() {
	window.scrollTo(0, 0);
}