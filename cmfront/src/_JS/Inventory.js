import React from 'react';
import CharacterSelection from './CharacterSelection.js';
import InventoryManager from './InventoryManager.js';

const API = 'https://localhost:44349/api/';
const CHAR_QUERY = 'characters/';
const INV_QUERY = 'items/';

class Inventory extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      characters: [],
      inventory: []
    };

    this.selectCharacter = this.selectCharacter.bind(this);
  }

  componentDidMount() {
    // get a list of all characters from the database
    fetch(API + CHAR_QUERY)
      .then(response => response.json())
      .then(data => this.setState({characters: data}))
  }

  async selectCharacter(character) {
    console.log("Selected character: " + character.name + " | Inventory ID: " + character.inventoryId);
    // get a list of specific items from the database
    fetch(API + INV_QUERY + character.inventoryId)
      .then(response => response.json())
      .then(data => this.setState({inventory: data}))
  }

  render() {
    if(!this.state.characters[0]) return null;

    return (
      <div className="Wrapper">
        <h1>Tabletop Campaign Manager</h1>

        <CharacterSelection characters={this.state.characters} onSelection={this.selectCharacter}/>
        <InventoryManager inventory={this.state.inventory} />
      </div>
      // InventoryRenderer
      /* 
      1. Find all owned items that match selection.InventoryID
      2. Find items that match owned item ID
      */
    );
  }
}

export default Inventory;
