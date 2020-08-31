import React from 'react';
import './../_CSS/PageLayout.css';

class InventoryManager extends React.Component {
  constructor(props) {
    super(props);

    // this.state = {
    //   inventory: []
    // };
  }

  // componentDidMount() {
  //   // character = this.props.character;
  //   // if (character == null) return null;

  //   var route = 'https://localhost:44349/api/items/' + this.props.character.inventoryid;

  //   fetch(route)
  //     .then(response => response.json())
  //     .then(data => this.setState({inventory: data}))
  // }

  render() {
    //const character = this.props.character;
    const inventory = this.props.inventory;
    if(!inventory[0]) return null;

    // counting algorithm goes here

    // map out item attributes (names, weights) into separate elements
    const inventoryListing = inventory.map(item =>
      <td key={item.itemId+"i"}> {item.name} </td>
    );
    const weightListing = inventory.map(item =>
      <td key={item.itemId+"w"}> {item.weight} </td>
    );

    return (
      <div className="Wrapper">
        <table>
          <tr>
            <th>Name</th>
            {inventoryListing}
          </tr>
          <tr>
            <th>Weight</th>
            {weightListing}
          </tr>
        </table>
      </div>
    );
  }
}

export default InventoryManager;
