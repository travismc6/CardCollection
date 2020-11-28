import React, { Fragment } from 'react';
import { Route, withRouter, RouteComponentProps } from 'react-router-dom';
import NavBar from './nav/NavBar';
import { Container } from 'semantic-ui-react';
import HomePage from '../features/Home/HomePage';

function App() {
  return (
    <Fragment>
      <Route exact path='/' component={HomePage} />
      <Route
        render={() => (
          <Fragment>
            <NavBar />
            <Container>

            </Container>


            {/* <Container style={{ marginTop: '7em' }}>
              <Route exact path='/activities' component={App} />
              <Route path='/activities/:id' component={App} />
              <Route
                path={['/createActivity', '/manage/:id']}
                component={App}
              />
            </Container> */}
          </Fragment>
        )}
      />
    </Fragment>
  );
}

export default App;
