import React from 'react';
import CharacterSelection from './CharacterSelection.js';

class InventoryManager extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      characters: [],
      selection: {}
    };

    this.selectCharacter = this.selectCharacter.bind(this);
  }

  componentDidMount() {
    fetch('https://localhost:44349/api/characters')
      .then(response => response.json())
      .then(data => this.setState({characters: data}))
  }

  async selectCharacter(character) {
    await this.setState({selection: character});
    console.log("Selected character: " + this.state.selection.name + " | Inventory ID: " + this.state.selection.inventoryId);
  }

  render() {
    if(!this.state.characters[0]) return null;
    //var guys = this.state.characters;

    // const playerCharacters = guys.map(chars =>
    //   <div>
    //     <button key={chars.CharacterId} onClick={(e) => this.selectCharacter(chars.name)}>{chars.name}, {chars.class}</button>
    //     <br />
    //   </div>
    // )

    return (
      <CharacterSelection characters={this.state.characters} onSelection={this.selectCharacter}/>
      // InventoryRenderer
      /* 
      1. Find all owned items that match selection.InventoryID
      2. Find items that match owned item ID
      */
    );
  }
}

export default InventoryManager;
