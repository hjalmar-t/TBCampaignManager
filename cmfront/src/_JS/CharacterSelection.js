import React from 'react';
import './../_CSS/PageLayout.css';

class CharacterSelection extends React.Component {
  constructor(props) {
    super(props);

    this.handleSelection = this.handleSelection.bind(this);
  }

  handleSelection(e) {
    this.props.onSelection(e);
  }

  render() {
    const characters = this.props.characters;


    const characterListing = characters.map(char =>
      <button key={char.characterId} onClick={(e) => this.handleSelection(char)}>{char.name}, {char.class}</button>
    )

      console.log("char render");
    return (
      <div className="Wrapper">
        {characterListing}
      </div>
    );
  }
}

export default CharacterSelection;
