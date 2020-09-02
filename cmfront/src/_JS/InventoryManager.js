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
    //console.log("test: " + inventory.length);
    // go through the selected inventory
      // for each member, count matching members from following members

    /*let deleteList = [];
    let countList = [];

    for(let i = 0; i < inventory.length; i++) {
      if(deleteList[i]) continue;

      let current = inventory[i];
      let count = 1;
      deleteList[i] = false;

      for(let j = i + 1; j < inventory.length; j++) {
        console.log("breakpoint: " + i + " and " + j);
        let next = inventory[j];
        if(current.itemId === next.itemId) {
          console.log("duplicate found");
          deleteList[j] = true;
          count++;
        }
      }

      countList[i] = count;
      //if(current.name === "Rations") console.log("Rations count: " + count);
    }

    for (let i = inventory.length-1; i >= 0; i--) {
      if(deleteList[i]) delete inventory[i];
    }
    
    console.log("test: " + countList);*/

    // map out item attributes (names, weights) into separate elements
    const inventoryListing = inventory.map(item =>
      <td key={item.itemId+"i"}> {item.name} </td>
    );
    const weightListing = inventory.map(item =>
      <td key={item.itemId+"w"}> {item.weight} </td>
    );
    // const countListing = countList.map((num, index) =>
    //   <td key={index}> {num} </td>
    // );

    return (
      <div className="Wrapper">
        <p></p>


        <table>
          <tbody>
            <tr>
              <th>Name</th>
              {inventoryListing}
            </tr>

            <tr>
              <th>Weight</th>
              {weightListing}
            </tr>

            {/* <tr>
              <th>Amount</th>
              {countListing}
            </tr> */}
          </tbody>
        </table>
      </div>
    );
  }
}

export default InventoryManager;
