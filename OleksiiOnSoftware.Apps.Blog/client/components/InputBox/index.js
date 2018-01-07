import React from 'react'

export class InputBox extends React.Component {
  constructor (props) {
    super(props)

    this.state = {
      value: 'Default1'
    }
  }

  handleChange = (event) => {
    this.setState({
      value: event.target.value
    })
  }

  render () {
    return (
      <div>
        <div>Update1:</div>
        <input type='text' value={this.state.value} onChange={this.handleChange} />
      </div>
    )
  }
}
