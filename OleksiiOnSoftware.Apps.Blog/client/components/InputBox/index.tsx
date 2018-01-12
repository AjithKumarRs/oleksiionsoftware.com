import * as React from "react";
import { SyntheticEvent } from "react";

interface State {
  value: string;
}

export class InputBox extends React.Component<{}, State> {
  constructor(props: any) {
    super(props);

    this.state = {
      value: "Default"
    };
  }

  handleChange = (event: SyntheticEvent<HTMLInputElement>) => {
    this.setState({
      value: event.currentTarget.value
    });
  };

  render() {
    return (
      <div>
        <div>Update:</div>
        <input
          type="text"
          value={this.state.value}
          onChange={this.handleChange}
        />
      </div>
    );
  }
}
