import React from 'react';
import './../_CSS/PageLayout.css';
import InventoryRow from './InventoryRow';

class InventoryManager extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      inventory: [],
      count: []
    };
  }

  static getDerivedStateFromProps(props, state) {
    let inventory = props.inventory;
    let deleteList = [];
    let countList = [];

    // for(let i = 0; i < inventory.length; i++) {
    //   //if(deleteList[i]) continue;

    //   let current = inventory[i];
    //   let count = 1;
    //   deleteList[i] = false;

    //   for(let j = i + 1; j < inventory.length; j++) {
    //     console.log("breakpoint: " + i + " and " + j);
    //     let next = inventory[j];
    //     if(current.itemId === next.itemId) {
    //       console.log("duplicate found");
    //       deleteList[j] = true;
    //       count++;
    //     }
    //   }

    //   countList[i] = count;
    // }

    // for (let i = inventory.length-1; i >= 0; i--) {
    //   if(deleteList[i]) delete inventory[i];
    // }

    return {inventory: inventory, count: countList};
  }

  render() {
    const inventory = this.state.inventory;
    const countList = this.state.count;
    if(!inventory[0]) return null;

    console.log(countList);
    const tableData = inventory.map(item =>
      <InventoryRow key={item.ownedItemId+"rlist"} item={item} />
    );

    // const tableData = compositionArray.map((array, i) =>
    //   <InventoryRow key={array.inventory[i].itemId+"rlist"} item={array.inventory[i]} count={array.countList[i]} />
    // );

    return (
      <div className="Wrapper">
        <table>
          <tbody>
            <tr key={"trh"}>
              <th key={"th1"}>Name</th>
              <th key={"th2"}>Weight</th>
              <th key={"th3"}>Number</th>
            </tr>
            {tableData}
          </tbody>
        </table>
      </div>
    );
  }
}

export default InventoryManager;
