// Framework
import * as React from "react";

// UI
import { Dimmer, Loader } from "semantic-ui-react";

// Styles
const style = {
  progress: {
    position: "fixed" as "fixed",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)"
  }
};

// Components
export const Progress = () => {
  return (
    <div style={style.progress}>
      <Dimmer active inverted>
        <Loader size="large">Loading...</Loader>
      </Dimmer>
    </div>
  );
};
