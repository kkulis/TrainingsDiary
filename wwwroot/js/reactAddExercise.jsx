

class AddExercise extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      series: 0
    }
  }
  render() {
    return (
      <div>
        <label for="numberofSeries">Number of Series</label>
        <div class="input-group">

          <input type="number" class="form-control" id="numberofSeries" placeholder="amount" />
          <div class="input-group-append">
            <button class="btn btn-primary" id="addSeries">+</button>
            <button class="btn btn-dark" id="removeSeries">-</button>
          </div>
        </div>

      </div>
    )
  }
}

ReactDOM.render(<AddExercise />, document.getElementById('AddExerciseContainer'));