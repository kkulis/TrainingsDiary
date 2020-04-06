
class AddExercise extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      numOfSeries: 0,
      series: [],
      dataGet: null,
      data: [],
    }

  }


  componentDidMount() {
    fetch(exerciseUrl)
    .then(response => response.json())
    .then(dataGet => this.setState({dataGet}));
  }


  onPlusClick = () => {
    console.log("+ click!");
    this.setState(({ numOfSeries }) => ({
      numOfSeries: numOfSeries + 1,
    }));
    this.state.series.push(<NewSeries />);
  }

  onMinusClick = () => {
    console.log("- click!");
    if (this.state.numOfSeries > 0) {
      this.setState(({ numOfSeries }) => ({
        numOfSeries: numOfSeries + -1
      }));
    }
    else {
      this.setState({
        numOfSeries: 0
      })
    }
    this.state.series.splice(-1, 1);
  }

  onSubmitSeriesClick = (serie) => {
    console.log("submit addex");
    var serieObj = {
      reps: serie.reps,
      weight: serie.weight
    };
    this.state.data.push(serieObj);
    //this.state.data.push('reps', serie.reps);
    //this.state.data.push('weight', serie.weight);

  }

  onSubmitClick = () => {

    var exerciseId = this.state.dataGet.id;
    var trainingId = this.state.dataGet.trainingId;

    var dataObj = {
      id: exerciseId,
      trainingId: trainingId,
      seriesViewModels: this.state.data
    }

    console.log("Submit all click");
    const data = dataObj

    fetch(exercisePostUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Succes", data);
      })
      .catch((error) => {
        console.error('error', error);
      })

      window.location.replace(redirectUrl);



  }

  render() {

    //const series = [];

    // for (var i = 0; i < this.state.numOfSeries; i++) {
    //   series.push(<NewSeries key={i} number={i} onChange={this.handleSeriesChange} value={this.state.series[i]}/>)
    //  };

    const series = this.state.series.map((serie, index) => (
      <NewSeries
        key={index}
        id={index}
        onSubmitSeriesClick={this.onSubmitSeriesClick}
        value={serie}
      />

    ));


    
    return (
      <div>
        <h2>{this.state.dataGet? this.state.dataGet.name: ""}</h2>
        <label htmlFor="numberofSeries">Number of Series</label>
        <div className="input-group">
          <InputNumber numOfSeries={this.state.numOfSeries} />
          <div className="input-group-append">
            <AddSeriesButton onPlusClick={this.onPlusClick} />
            <RemoveSeriesButton onMinusClick={this.onMinusClick} />
          </div>
        </div>
        <hr className="style 5"></hr>
        <div>
          {series}
        </div>
        <hr className="style 5"></hr>
        <div className="text-center">
          <SubmitButton onSubmitClick={this.onSubmitClick} />
        </div>

      </div>
    );
  }
}

// class Name extends React.Component {
//   constructor(props) {
//     super(props)
//     this.state = {
//       dataGet: props.dataGet
//     }
//   }
//   render() {
//    // const name = this.props.dataGet.name;
//     return(
//       <h2>{this.props.dataGet.name}</h2>
//     )

//   }

// }


class SubmitButton extends React.Component {
  constructor(props) {
    super(props)

    this.onSubmitClick = props.onSubmitClick;

  }

  render() {
    return (
      <button className="btn btn-primary" onClick={this.onSubmitClick}>Submit</button>
    )
  }
}

class AddSeriesButton extends React.Component {
  constructor(props) {
    super(props)

    this.onPlusClick = props.onPlusClick;

  }

  render() {
    return (
      <button className="btn btn-primary" onClick={this.onPlusClick}>+</button>
    )
  }
}

class RemoveSeriesButton extends React.Component {
  constructor(props) {
    super(props)

    this.onMinusClick = props.onMinusClick;

  }

  render() {
    return (
      <button className="btn btn-dark" onClick={this.onMinusClick}>-</button>
    )
  }
}


class InputNumber extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      numOfSeries: props.numOfSeries
    }
  }
  render() {
    return (
      <input type="number" className="form-control" id="numberOfSeries" placeholder="amount" value={this.props.numOfSeries}></input>
    );
  }

}

{/*---------------------------------------------NEW SERIES----------------------------------------*/ }

class NewSeries extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      reps: 0,
      weight: 0
    }
  }

  onPlusRepsClick = () => {
    console.log("Plus Reps Click!")
    this.setState(({ reps }) => ({
      reps: reps + 1
    }));

  }

  onPlusWeightClick = () => {
    console.log("Plus Weight Click!")
    this.setState(({ weight }) => ({
      weight: weight + 5
    }));

  }

  onMinusRepsClick = () => {
    console.log("- click!");
    if (this.state.reps > 0) {
      this.setState(({ reps }) => ({
        reps: reps - 1
      }));
    }
    else {
      this.setState({
        reps: 0
      })
    }
  }

  onMinusWeightClick = () => {
    console.log("- Weight click!");
    if (this.state.weight > 0) {
      this.setState(({ weight }) => ({
        weight: weight - 5
      }));
    }
    else {
      this.setState({
        weight: 0
      })
    }
  }

  onSubmitClick = () => {
    console.log("Submit click");
    const reps = this.state.reps;
    const weight = this.state.weight;

    if (!reps) {
      return;
    }
    this.props.onSubmitSeriesClick({ reps: reps, weight: weight })
    this.setState(
      {
        reps: 0,
        weight: 0
      }
    )

  }


  render() {
    return (
      <div className="row">

        <div className="col">
          <label htmlFor="numberofReps">Number of Reps</label>
          <div className="input-group">
            <InputRepsNumber reps={this.state.reps} />
            <div className="input-group-append">
              <AddRepsButton onPlusRepsClick={this.onPlusRepsClick} />
              <RemoveRepsButton onMinusRepsClick={this.onMinusRepsClick} />
            </div>
          </div>
        </div>
        <div className="col">
          <label htmlFor="numberofWeight">Weight</label>
          <div className="input-group">
            <InputWeightNumber weight={this.state.weight} />
            <div className="input-group-append">
              <AddWeightButton onPlusWeightClick={this.onPlusWeightClick} />
              <RemoveWeightButton onMinusWeightClick={this.onMinusWeightClick} />
            </div>
          </div>
        </div>
        <div classNane="col">
          <label>--</label>
          <div className="input-group">
            <SubmitSeriesButton onSubmitClick={this.onSubmitClick} />
          </div>
        </div>
        <hr className="style 5"></hr>
      </div>
    );
  }
}

class SubmitSeriesButton extends React.Component {
  constructor(props) {
    super(props)

    this.onSubmitClick = props.onSubmitClick
  }
  render() {
    return (
      <div>
        <button className="btn btn-success" type="submit" value="Post" onClick={this.onSubmitClick}>Add</button>
      </div>
    )
  }
}

class RemoveWeightButton extends React.Component {
  constructor(props) {
    super(props)

    this.onMinusWeightClick = props.onMinusWeightClick;

  }

  render() {
    return (
      <button className="btn btn-dark" onClick={this.onMinusWeightClick}>-</button>
    )
  }
}

class AddWeightButton extends React.Component {
  constructor(props) {
    super(props)

    this.onPlusWeightClick = props.onPlusWeightClick;

  }

  render() {
    return (
      <button className="btn btn-primary" onClick={this.onPlusWeightClick}>+</button>
    )
  }
}

class InputWeightNumber extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      weight: props.weight
    }
  }
  render() {
    return (
      <input type="number" className="form-control" id="Weight" placeholder="amount" value={this.props.weight}></input>
    );
  }
}

class InputRepsNumber extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      reps: props.reps
    }
  }
  render() {
    return (
      <input type="number" className="form-control" id="numberOfReps" placeholder="amount" value={this.props.reps}></input>
    );
  }
}

class AddRepsButton extends React.Component {
  constructor(props) {
    super(props)

    this.onPlusRepsClick = props.onPlusRepsClick;

  }

  render() {
    return (
      <button className="btn btn-primary" onClick={this.onPlusRepsClick}>+</button>
    )
  }
}

class RemoveRepsButton extends React.Component {
  constructor(props) {
    super(props)

    this.onMinusRepsClick = props.onMinusRepsClick;

  }

  render() {
    return (
      <button className="btn btn-dark" onClick={this.onMinusRepsClick}>-</button>
    )
  }
}



ReactDOM.render(
  <AddExercise url={exerciseUrl} postUrl={exercisePostUrl}
   redirectUrl ={redirectUrl} pollInterval={2000}/>,
  document.getElementById('AddExerciseContainer')
);