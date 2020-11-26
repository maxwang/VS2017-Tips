1.  Avoid using anonymouse function

Because we are passing anonymous functions, React will always re-render since it receives a new anonymous function as a prop which it is unable to compare to the previous anonymous function (since they are both anonymous)

```jsx
render() { 
    return 
      < button onClick={() => this.handleClick()} /> 
  }
```

2. setState is asynomouse, you could use call back 

Because of its asynchronous nature, setState accepts a second argument that is a function that it invokes after the state has been updated.

```js
handleFirstNameChange = (event) => {
  this.setState({firstName: event.currentTarget.value}, () => {
    console.log(this.state.firstName);
  });
}
```

Official recommended way is to use `useEffect` hook:

```js
const [count, setCount] = useState(0)

useEffect(() => {
  callback(count); // Will be called when the value of count changes
}, [count, callback]);

const handleChange = value => {
  setCount(value)
};
```

3. Correct way set state based on pre value

```js
handleChange = count => {
  this.setState({ count: this.state.count + 1 }); // Relying on current value of the state to update it
};
```

in some cases the value of count may not be properly updated at the moment when the new state is being set, which will result in the new state value to be set incorrectly. This happens because the state updates are batched in React, so in case there are multiple calls modifying the same variable, such updates can produce unexpected results. A safer way here is to use the funcitonal form of setState

```js
increment = () => {
  this.setState(state => ({ count: state.count + 1 })); // The latest state value is used
};
```


