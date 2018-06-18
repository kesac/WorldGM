import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { WorldInfo } from './components/WorldInfo';
import { AthleteList } from './components/AthleteList';
import { AthleteInfo } from './components/AthleteInfo';
import { TeamList } from './components/TeamList'

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/worldinfo' component={ WorldInfo } />
    <Route path='/athletes' component={ AthleteList } />
    <Route path='/athlete/:id' component={ AthleteInfo } />
    <Route path='/teams' component={ TeamList } />
</Layout>;
