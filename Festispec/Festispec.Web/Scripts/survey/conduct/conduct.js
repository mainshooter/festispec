class SurveyConductor {

	// SCOPE - container
    constructor(identifer) {
		this.scope = document.querySelector(identifer);
		this.addListeners();
	}

	addListeners() {
		this.scope.querySelector("#saveSurvey").addEventListener('click', (e) => this.saveSurveyAnswersToLocalStorage());
	}

    getQuestion() {
        let questions = [];

	}

	getAnswers() {

	}

	isRequired(inputElement) {
		let containsAttribute = inputElement.containsAttribute("required");
		if (containsAttribute === true) {
			let attributeValue = inputElement.getAttribute("required");
			return attributeValue;
		}
		return false;
	}

	saveSurveyAnswersToLocalStorage() {
		
	}
}


(function () {

})();


let surveyCon = new SurveyConductor(".survey-container");