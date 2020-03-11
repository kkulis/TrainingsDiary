
class AddExercise extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      numOfSeries: 0
    }
  }

  onPlusClick = () => {
    console.log("+ click!");
    this.setState(({ numOfSeries }) => ({
      numOfSeries: numOfSeries + 1
    }));
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
  }
  render() {

    return (
      <div>
        <label htmlFor="numberofSeries">Number of Series</label>
        <div className="input-group">
          <InputNumber numOfSeries={this.state.numOfSeries} />
          <div className="input-group-append">
            <AddSeriesButton onPlusClick={this.onPlusClick} />
            <RemoveSeriesButton onMinusClick={this.onMinusClick} />
          </div>
        </div>

      </div>
    );
  }
}

class NewSeries extends React.Component{

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

ReactDOM.render(<AddExercise />, document.getElementById('AddExerciseContainer'));