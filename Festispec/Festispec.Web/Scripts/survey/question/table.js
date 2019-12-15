class TableQuestion {

	constructor(tableId, inputName) {
		this.scope = document.querySelector(tableId);
		this.inputField = document.querySelector("input[name='" + inputName + "']");
		this.addListners();
	}

	addListners() {
		this.scope.addEventListener('change', (event) => {
			let tableAnswerRows = this.scope.querySelectorAll("tbody tr");
		});
	}

	setNewValues(value) {
		this.inputField.value = value;
	}
}